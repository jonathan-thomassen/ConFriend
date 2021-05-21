// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
window.onscroll = function () { myFunction() };


// Get the header

var header = document.getElementById("HeadField");


// Get the offset position of the navbar

var sticky = header.offsetTop;


// Add the sticky class to the header when you reach its scroll position. Remove "sticky" when you leave the scroll position

function myFunction() {

    if (window.pageYOffset > sticky) {

        header.classList.add("sticky");

    } else {

        header.classList.remove("sticky");

    }

}