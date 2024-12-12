function afisPagina1() {
    document.getElementById('PostariPage').style.display = 'flex';
    document.getElementById('MentiuniPage').style.display = 'none';
    document.getElementById('NotitePage').style.display = 'none';
    sessionStorage.setItem('activeSection', 'Postari');
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
