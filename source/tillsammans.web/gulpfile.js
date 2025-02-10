import gulp from 'gulp';
import clean from 'gulp-clean';
import pug from 'gulp-pug';
import yargs from 'yargs'
import { hideBin } from 'yargs/helpers'
import fs from 'node:fs';
import { statSync } from 'node:fs'
import path from 'path'
import concat from 'gulp-concat'
import uglify from 'gulp-uglify'
import merge from 'merge-stream'
import changed from 'gulp-changed'
import autoprefixer from 'gulp-autoprefixer'
import rename from 'gulp-rename'
import csso from 'gulp-csso'

const argv = yargs(hideBin(process.argv))
  .option('environment', {
    alias: 'e',
    type: 'string',
    description: 'What environment to build for [production || test]',
    demandOption: false
  }).argv;
  console.log(`Environment: ${argv.environment}`);

function tryStatSync(filePath) {  
  try {
    return statSync(filePath)
  } catch(e) {
    return undefined // instead of `new Stats()` here
  }
}

function getFolders(dir) 
{
  try {
    var folders = []
    const files = fs.readdirSync(dir);

    files.forEach(file => {
      const filePath = path.join(dir, file);
      const stats = tryStatSync(filePath)
      if (stats && stats.isDirectory()) { 
        folders.push(file)
      }
      
    });
    return folders
  } catch (err) {
    console.error('Error:', err);
  }
  
}

gulp.task('clean', function () {
  return gulp.src('./public', { read: false, allowEmpty: true })
      .pipe(clean());
});


gulp.task('pug', function () {
  return gulp.src('./source/pug/pages/**/*.pug')
      .pipe(pug({ pretty: true }))
      .pipe(gulp.dest('./public'));
});

gulp.task('scripts', function () 
{
  var folders= getFolders("./source/javascripts")
  folders = folders.filter(function (folder) { return folder !== 'config'; });
  var tasks = folders.map(function(folder) 
  {
    const source = ['./source/javascripts/'+folder+'/*.js'];
    var stream = gulp.src(source).pipe(concat(folder + '.js'))
    if (argv.environment=='production') stream=stream.pipe(uglify()); 
    return stream.pipe(gulp.dest('./public/javascripts/'));
  });

  if (argv.environment=='production') tasks.push(gulp.src('./source/javascripts/config/production.js').pipe(concat('config.js')).pipe(gulp.dest('./public/javascripts/')));
  else tasks.push(gulp.src('./source/javascripts/config/development.js').pipe(concat('config.js')).pipe(gulp.dest('./public/javascripts/')));
  return merge(tasks);
});

gulp.task('styles', function () {
  const source = './source/stylesheets/*.css';
  return gulp.src(source)
      .pipe(changed(source))
      .pipe(autoprefixer({
          overrideBrowserslist: ['last 2 versions'],
          cascade: false
      }))
      .pipe(rename({
          extname: '.min.css'
      }))
      .pipe(csso())
      .pipe(gulp.dest('./public/stylesheets/'))
    });


gulp.task('images', function () {
  return gulp.src('./source/images/**/*.*',  { encoding: false })
      .pipe(gulp.dest('./public/images'));
});

gulp.task('watch', function () {
  gulp.watch('source/pug/**/*.pug', gulp.series('pug'));
  gulp.watch('source/stylesheets/**/*.css', gulp.series('styles'));
  gulp.watch('source/javascripts/**/*.js', gulp.series('scripts'));
  gulp.watch('source/images/**/*.*', gulp.series('images'));
});

gulp.task('default', gulp.series('clean', 'pug','scripts','styles','images', function (done) {
  done();
}));
