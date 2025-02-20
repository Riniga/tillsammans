/// <binding BeforeBuild='default' />
import gulp from 'gulp';
import clean from 'gulp-clean';
import pug from 'gulp-pug';

gulp.task('clean', function () {
    return gulp.src('./public', { read: false, allowEmpty: true })
        .pipe(clean());
});

gulp.task('pug', function () {
    return gulp.src('./source/pug/pages/**/*.pug')
        .pipe(pug({ pretty: true }))
        .pipe(gulp.dest('./public'));
});

gulp.task('scripts', function () {
    return gulp.src('./source/scripts/**/*.js')
        .pipe(gulp.dest('./public/scripts'));
});

gulp.task('styles', function () {
    return gulp.src('./source/styles/**/*.css')
        .pipe(gulp.dest('./public/styles'));
});


gulp.task('images', function () {
    return gulp.src('./source/images/**/*.*')
        .pipe(gulp.dest('./public/images'));
});

gulp.task('configurations', function () {
    return gulp.src('./source/configurations/**/*.js')
        .pipe(gulp.dest('./public/configurations'));
});

gulp.task('data', function () {
    return gulp.src('./source/data/**/*.json')
        .pipe(gulp.dest('./public/data'));
});

gulp.task('watch', function () {
    gulp.watch('source/pug/**/*.pug', gulp.series('pug'));
    gulp.watch('source/styles/**/*.css', gulp.series('styles'));
    gulp.watch('source/scripts/**/*.js', gulp.series('scripts'));
});

gulp.task('default', gulp.series('clean', 'pug', 'styles', 'scripts', 'images', 'configurations','data', function (done) {
    done();
}));
