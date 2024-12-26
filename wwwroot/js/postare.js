function arataPostareDetaliata(id) { 
    window.scrollTo({ top: 0, behavior: 'smooth' });
    document.getElementById("PostareDetaliata").style.display = "flex";
    document.getElementById("pozaDT").src = document.getElementById("img-" + id).src;
    document.getElementById("descrierePostare").style.width = document.getElementById("pozaDT").scrollWidth + "px";
    if (document.getElementById("pozaDT").scrollWidth < 250) {
        document.getElementById("descrierePostare").style.width = 250 + "px";
    }
    document.getElementById("descReal").innerHTML = document.getElementById("descAscuns-"+id).innerHTML
}
function xPostareDeschisa() {
    document.getElementById("PostareDetaliata").style.display = "none";
}
function openMeniuPostare() {
    document.getElementById("meniuDeschisPostare").style.display = "flex";
}