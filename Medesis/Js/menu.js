$(document).ready(function () {

    $('.btn').click(function () {
        $(this).toggleClass("click");
        $('.sidebar').toggleClass("show");
    });

    $('.medicos-btn').click(function () {
        $('nav ul .medicos-show').toggleClass("show");
        $('nav ul .first').toggleClass("rotate");
    });

    $('.pacientes-btn').click(function () {
        $('nav ul .pacientes-show').toggleClass("show1");
        $('nav ul .second').toggleClass("rotate");
    });

    $('.consultas-btn').click(function () {
        $('nav ul .consultas-show').toggleClass("show2");
        $('nav ul .third').toggleClass("rotate");
    });

    $('nav ul li').click(function () {
        $(this).addClass("active").siblings().removeClass("active");
    });
 
});
