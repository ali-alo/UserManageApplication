// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let state = false;
function toggleAll() {
    let elements = document.getElementsByClassName("user-checkbox");
    if (state)
        for (var i = 0; i < elements.length; i++)
            elements[i].checked = false;
    else
        for (var i = 0; i < elements.length; i++)
            elements[i].checked = true;
    state = !state;
}