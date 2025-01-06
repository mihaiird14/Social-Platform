function afisUrmaritori() {
    xFollow();
    document.getElementById("threadbox").style.display = "none";
    const comTh = document.querySelectorAll('[id^="CommentBox-"]');
    comTh.forEach(element => {
        element.style.display = "none";
    });
    const comP = document.querySelectorAll('[id^="PostareDetaliata-"]');
    comP.forEach(element => {
        element.style.display = "none";
    });
    const d = document.getElementById("FisaUrmaritori");
    d.style.display = 'flex';
    
    sessionStorage.setItem('fisaUrmaritori', 'da');
}
function afisUrmariri() {
    xFollow();
    document.getElementById("threadbox").style.display = "none";
    const comTh = document.querySelectorAll('[id^="CommentBox-"]');
    comTh.forEach(element => {
        element.style.display = "none";
    });
    const comP = document.querySelectorAll('[id^="PostareDetaliata-"]');
    comP.forEach(element => {
        element.style.display = "none";
    });
    const d = document.getElementById("FisaUrmariri");
    d.style.display = 'flex';    
    sessionStorage.setItem('fisaUrmaritori', 'da2');
}
function xFollow() {
    const d = document.getElementById("FisaUrmaritori");
    document.getElementById("FisaUrmariri").style.display='none';
    d.style.display = 'none';
    sessionStorage.setItem('fisaUrmaritori', 'nu');
}
document.addEventListener("DOMContentLoaded", function () {
    const xf = sessionStorage.getItem('fisaUrmaritori');
    if (xf === 'da') {  
        afisUrmaritori();
    }
    else if (xf === "da2") {
        afisUrmariri();
    }
    else
    {
        xFollow();
    }
});