const {
  src,
  dest,
  parallel,
  series,
  watch
} = require('gulp');

const uglify = require('gulp-uglify');
const rename = require('gulp-rename');
const sass = require('gulp-sass');
const autoprefixer = require('gulp-autoprefixer');
const cssnano = require('gulp-cssnano');
const concat = require('gulp-concat');
const clean = require('gulp-clean');
//const imagemin = require('gulp-imagemin');
const changed = require('gulp-changed');
const pug = require('gulp-pug');

function clear() {
  return src('./public/*', {read: false})
      .pipe(clean());
}

function js() {
  const source = './source/javascripts/*.js';

  return src(source)
      .pipe(changed(source))
      .pipe(concat('bundle.js'))
      .pipe(uglify())
      .pipe(rename({
          extname: '.min.js'
      }))
      .pipe(dest('./public/javascripts/'))
}

function css() {
  const source = './source/stylesheets/*.css';

  return src(source)
      .pipe(changed(source))
      .pipe(autoprefixer({
          overrideBrowserslist: ['last 2 versions'],
          cascade: false
      }))
      .pipe(rename({
          extname: '.min.css'
      }))
      .pipe(cssnano())
      .pipe(dest('./public/stylesheets/'))
}

function html() {
  return src('./source/pug/pages/**/*.pug')
  .pipe(pug({pretty: true}))
  .pipe(dest('./public'));
}

function img() {
  return src('./source/images/**/*.*')
      //.pipe(imagemin())
      .pipe(dest('./public/images'));
}

function watchFiles() {
  watch('./source/stylesheets/**/*.*', css);
  watch('./source/javascripts/**/*.*', js);
  watch('./source/images/**/*.*', img);
  watch('./source/pug/**/*.*', html);
}

// Tasks to define the execution of the functions simultaneously or in series

exports.clean = clear;
exports.js = js;
exports.css = css;
exports.html = html;
exports.img = img;

exports.watch = watchFiles;
exports.default = series(clear, parallel(html, js, css, img));