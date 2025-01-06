function arataPostareDetaliata(id) { 
    window.scrollTo({ top: 0, behavior: 'smooth' });
    xFollow();
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
    document.getElementById("dropdownPostare-" + id).style.display = "flex";
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
function deletePostComm(id) {
    var comBox = document.querySelector('.afisComPost-' + id);

    if (comBox) {
        var PostH = comBox.scrollHeight;
        var wBox = comBox.scrollWidth;
        document.querySelector('.delete-com-post-' + id).style.display = "flex";
        comBox.style.display = "none";
        document.querySelector('.delete-com-post-' + id).style.height = PostH + "px";
    }
}
function xDeleteCom(id) {

    document.querySelector('.afisComPost-' + id).style.display = 'block';
    document.querySelector('.delete-com-post-' + id).style.display = 'none';
}
function editComPost(id) {
    document.querySelector('.editComBoxPost-' + id).style.display = "flex";
    document.querySelector('.editComBoxPost-' + id).style.height = document.querySelector('.afisComPost-' + id).scrollHeight + "px";
    document.querySelector('.afisComPost-' + id).style.display = "none";

}
function xEditPostCom(id) {
    document.querySelector('.afisComPost-' + id).style.display = "block";
    document.querySelector('.editComBoxPost-' + id).style.display = "none";
}
