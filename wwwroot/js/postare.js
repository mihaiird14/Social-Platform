function arataPostareDetaliata(id) { 
    window.scrollTo({ top: 0, behavior: 'smooth' });
    document.getElementById("PostareDetaliata-"+id).style.display = "flex";
    document.getElementById("pozaDT-"+id).src = document.getElementById("img-" + id).src;
    document.getElementById("descrierePostare-"+id).style.width = document.getElementById("pozaDT-"+id).scrollWidth + "px";
    if (document.getElementById("pozaDT-"+id).scrollWidth < 250) {
        document.getElementById("descrierePostare-"+id).style.width = 250 + "px";
    }
    document.getElementById("descReal-"+id).innerHTML = document.getElementById("descAscuns-" + id).innerHTML
    document.getElementById("ConfirmareStergere").style.display = "none";
}
function xPostareDeschisa(id) {
    document.getElementById("PostareDetaliata-"+id).style.display = "none";
    document.querySelectorAll('.ddPostare').forEach(menu => {
        menu.style.display = "none";
    });
}
function openDropdownPostari(id) {
    if (document.getElementById("dropdownPostare-" + id).style.display === "flex") {
        document.getElementById("dropdownPostare-" + id).style.display = "none";
    }
    else {
        document.getElementById("dropdownPostare-" + id).style.display = "flex";
    }
}
function openConfirmareStergerePostare(id) {
    xPostareDeschisa(id);
    document.getElementById("ConfirmareStergere-"+id).style.display = "flex";
    window.scrollTo({ top: 0, behavior: 'smooth' });
}
function inchideConfirmaStergerePostare(id) {
    document.getElementById("ConfirmareStergere-" + id).style.display = "none";
}
function openEditeazaPostarea(id) {
    xPostareDeschisa(id);
    document.getElementById("EditPostare-" + id).style.display = "flex";
    document.getElementById("pozaDT2-" + id).src = document.getElementById("img-" + id).src;
    document.getElementById("descrierePostare2-" + id).style.width = document.getElementById("pozaDT2-" + id).scrollWidth + "px";
    if (document.getElementById("pozaDT2-" + id).scrollWidth < 250) {
        document.getElementById("descrierePostare2-" + id).style.width = 250 + "px";
    }
    document.getElementById("descReal2-" + id).innerHTML = document.getElementById("descAscuns-" + id).innerHTML
}
function inchideEditPostare(id) {
    const edtPost = document.querySelectorAll('[id^="EditPostare"]');
    edtPost.forEach(x => {
        x.style.display = "none";
    });
}