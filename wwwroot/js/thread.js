﻿function threadfunction() {
    document.getElementById('threadbox').style.display = 'flex';
    xFollow();
    sessionStorage.setItem('addThread', 'on');
 
}
function threadCloseFunction() {
    document.getElementById('threadbox').style.display = 'none';
    sessionStorage.setItem('addThread', 'off');
}
function EditthreadCloseFunction(id) {
    document.getElementById('editThreadBox-'+id).style.display = 'none';

}
function openMenuThreads(id) {
    xFollow();
    if (document.getElementById("dropdownMenuThread-" + id).style.display === "block") {
        document.getElementById("dropdownMenuThread-" + id).style.display = "none";
    }
    else {
        document.getElementById("dropdownMenuThread-" + id).style.display = "block";
    }
}
document.addEventListener("DOMContentLoaded", function () {

    const buttons = document.querySelectorAll('.delete-thread-form');
    const butonEdit = document.querySelectorAll('.edit-thread-form');

    buttons.forEach(button => {

        button.addEventListener('click', function (event) {
            event.preventDefault();
            const threadId = this.getAttribute('data-thread-id');
            deleteCloseFunction(threadId);
        });
    });
    butonEdit.forEach(b => {
        b.addEventListener('click', function (event) {
            event.preventDefault();
            const threadId = this.getAttribute('data-thread-id');
            editCloseFunction(threadId);
        })
    })
});
function deleteCloseFunction(threadId) {
    var threadBox = document.querySelector('.thread-' + threadId);

    if (threadBox) {
        var threadBoxHeight = threadBox.scrollHeight;
        document.querySelector('.delete-' + threadId).style.height = threadBoxHeight + 'px';
        document.querySelector('.thread-' + threadId).style.display = 'none';
        document.querySelector('.delete-' + threadId).style.display = 'flex';
    }  
}
function editCloseFunction(threadId) {
    var threadBox = document.querySelector('.thread-' + threadId);
    
    if (threadBox) {
        var threadBoxHeight = threadBox.scrollHeight;
        var wBox = threadBox.scrollWidth;
        var tarea = document.querySelector('.note-textarea-edit-'+threadId );
        tarea.style.width = '400px';
        tarea.style.height = threadBoxHeight - 100 + 'px';
        document.querySelector('.thread-' + threadId).style.display = 'none';
        document.querySelector('.editThreadBox-' + threadId).style.display = 'flex';
        document.querySelector('.editThreadBox-' + threadId).style.height = threadBoxHeight + 'px';
    }
}
document.addEventListener("DOMContentLoaded", function () {
    const icons = document.querySelectorAll('#xDelete');
    icons.forEach(icon => {
        icon.addEventListener('click', function (e) {
            const threadId = this.getAttribute('data-thread-id');
            document.querySelector('.thread-' + threadId).style.display = 'flex';
            document.querySelector('.delete-' + threadId).style.display = 'none';
            document.querySelector('.editThreadBox-' + threadId).style.display = 'none';
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const icons = document.querySelectorAll('#xEdit');
    icons.forEach(icon => {
        icon.addEventListener('click', function (e) {
            const threadId = this.getAttribute('data-thread-id');
            document.querySelector('.thread-' + threadId).style.display = 'flex';
            document.querySelector('.editThreadBox-' + threadId).style.display = 'none';
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const ax = sessionStorage.getItem('addThread');
    if (ax === 'on') {
        threadfunction();
    } else {
        threadCloseFunction();
    }
});