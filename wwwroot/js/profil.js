function afisPagina1() {
    document.getElementById('PostariPage').style.display = 'flex';
    document.getElementById('MentiuniPage').style.display = 'none';
    document.getElementById('NotitePage').style.display = 'none';
    sessionStorage.setItem('activeSection', 'Postari');
    const elemente = document.querySelectorAll('[id^="PostareDetaliata"]');
    elemente.forEach(element => {
        element.style.display = "none";
    });
    document.querySelectorAll('.ddPostare').forEach(menu => {
        menu.style.display = "none";
    });
    const sterge = document.querySelectorAll('[id^="ConfirmareStergere"]');
    sterge.forEach(x => {
        x.style.display = "none";
    });
    const ddTh = document.querySelectorAll('[id^="dropdownMenuThread"]');
    ddTh.forEach(x => {
        x.style.display = "none";
    });
    document.getElementById("threadbox").style.display = "none";
    const edtPost = document.querySelectorAll('[id^="EditPostare"]');
    edtPost.forEach(x => {
        x.style.display = "none";
    });
}

function afisPagina2() {
    document.getElementById('PostariPage').style.display = 'none';
    document.getElementById('MentiuniPage').style.display = 'flex';
    document.getElementById('NotitePage').style.display = 'none';
    sessionStorage.setItem('activeSection', 'Mentiuni');
}

function afisPagina3() {
    document.getElementById('PostariPage').style.display = 'none';
    document.getElementById('MentiuniPage').style.display = 'none';
    document.getElementById('NotitePage').style.display = 'flex';
    sessionStorage.setItem('activeSection', 'Threads');
    const elemente = document.querySelectorAll('[id^="PostareDetaliata"]');
    elemente.forEach(element => {
        element.style.display = "none";
    });
    document.querySelectorAll('.ddPostare').forEach(menu => {
        menu.style.display = "none";
    });
    const sterge = document.querySelectorAll('[id^="ConfirmareStergere"]');
    sterge.forEach(x => {
        x.style.display = "none";
    });
    const ddTh = document.querySelectorAll('[id^="dropdownMenuThread"]');
    ddTh.forEach(x => {
        x.style.display = "none";
    });
    document.getElementById("threadbox").style.display = "none";
    const edtPost = document.querySelectorAll('[id^="EditPostare"]');
    edtPost.forEach(x => {
        x.style.display = "none";
    });
}
function postareFunction() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
    document.getElementById("addPostareDiv").style.display = "flex";
}
function xPostare() {
    document.getElementById("addPostareDiv").style.display = "none";
}
document.addEventListener("DOMContentLoaded", function () {
    const activeSection = sessionStorage.getItem('activeSection');
    if (activeSection === 'Postari') {
        afisPagina1();
    } else if (activeSection === 'Mentiuni') {
        afisPagina2();
    } else if (activeSection === 'Threads') {
        afisPagina3();
    } else {
        afisPagina1();
    }
});

