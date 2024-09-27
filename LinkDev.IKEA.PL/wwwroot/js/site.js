// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var searchInp = document.getElementById("searchInp");

searchInp.addEventListener("keyup", function () {
    var searchValue = searchInp.value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `https://localhost:7086/Employee/Index?search=${searchValue}`);


    xhr.send();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE) {  // XMLHttpRequest.DONE == 4
            if (xhr.status == 200) {
                document.body.innerHTML = xhr.responseText;
            }
            else {
                alert('Something else other than 200 was returned');
            }
        }
    };



});
