function CommentBox(event, id) {
    if (event != null) {
        event.preventDefault();
    }
    const Box = document.getElementById("CommentBox-"+id);
    Box.style.display = 'flex';
    Box.scrollIntoView({
        behavior: "smooth",
        block: "center",
        inline: "nearest"
    });
    const th = document.getElementsByClassName("thread-" + id);
    const text = th[0].querySelector("#ThreadContinut");
    const BoxText = Box.querySelector("#ComBoxContinut");
    const paragraf = BoxText.querySelector("#continut");
    const nume = th[0].querySelector(".profile-name");
    paragraf.innerHTML = text.innerHTML;
    const poza = th[0].querySelector(".profile-picture");
    const BoxPoza = BoxText.querySelector("#comImg");
    BoxPoza.setAttribute("src", poza.getAttribute("src"));
    BoxText.querySelector("#username").innerHTML = nume.innerHTML;
    BoxText.querySelector("#ComOra").innerHTML = th[0].querySelector("#ora").innerHTML;
    sessionStorage.setItem('Comentarii', 'da');
    sessionStorage.setItem('Id', id);
}
function  xCom(id){
    document.getElementById("CommentBox-"+id).style.display = 'none';
    sessionStorage.setItem('Comentarii', 'nu');
}
document.addEventListener("DOMContentLoaded", function () {
    const dropdownToggles = document.querySelectorAll('.dropdown-toggle-comentarii');

    dropdownToggles.forEach(toggle => {
        toggle.addEventListener('click', function (e) {
            e.stopPropagation();
            const menu = this.nextElementSibling;

            if (menu.style.display == "block") {
                menu.style.display = "none";
            } else {
                menu.style.display = "block";
            }
        });
    });
    document.addEventListener('click', function () {
        document.querySelectorAll('.dropdown-menu-comentarii').forEach(menu => {
            menu.style.display = "none";
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    if (sessionStorage.getItem("Comentarii") === 'nu') {
        xCom();
    }
    if (sessionStorage.getItem("Comentarii") === 'da') {
        const id = sessionStorage.getItem("Id");
        CommentBox(null, id);
    }
});
document.addEventListener("DOMContentLoaded", function () {
    const bt = document.querySelectorAll('.delete-thread-form-com');
    const ed = document.querySelectorAll('.edit-thread-form-com');
    bt.forEach(t => {
        t.addEventListener('submit', function (e) {
            e.preventDefault();
            const comId = this.getAttribute('data-com-id');
            document.querySelector('.delete-com-' + comId).style.height = document.querySelector('.afisCom-' + comId).scrollHeight + "px";
            document.querySelector('.delete-com-' + comId).style.display = 'flex';
            document.querySelector('.afisCom-' + comId).style.display = 'none';
            document.querySelector('.editComBox-' + comId).style.display = 'none';
        });
    });
    ed.forEach(edd => {
        edd.addEventListener('submit', function (e) {
            e.preventDefault();
            const comId = this.getAttribute('data-com-id');
            document.querySelector('.editComBox-' + comId).style.height = document.querySelector('.afisCom-' + comId).scrollHeight + "px";
            document.querySelector('.delete-com-' + comId).style.display = 'none';
            document.querySelector('.afisCom-' + comId).style.display = 'none';
            document.querySelector('.editComBox-' + comId).style.display = 'flex';
          
        });
    });
});
document.addEventListener("DOMContentLoaded", function () {
    const xds = document.querySelectorAll('#xComDelete');
    const xed = document.querySelectorAll('#xComEdit');
    xds.forEach(xd => {
        xd.addEventListener('click', function (e) {      
            const comId = this.getAttribute('data-com-id');
             document.querySelector('.delete-com-' + comId).style.display = 'none';
            document.querySelector('.afisCom-' + comId).style.display = 'block';
        });
    });
    xed.forEach(xd => {
        xd.addEventListener('click', function (e) {
            const comId = this.getAttribute('data-com-id');
            document.querySelector('.editComBox-' + comId).style.display = 'none';
            document.querySelector('.afisCom-' + comId).style.display = 'block';
        });
    });
});