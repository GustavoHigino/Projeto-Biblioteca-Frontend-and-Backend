const esperar = ms=>new Promise(resolve=> setTimeout(resolve, ms));
window.document.addEventListener("click",async e=>{
    const click = e.target
    const login= document.querySelector("#Mlogin")
    const registro=document.querySelector("#Mregistro")
    if(login.classList.contains("on")&&click.id==="Lentrar"){

    }
    if(login.classList.contains("on")&&click.id==="Lregistrar"){
        login.style.transform="translateX(-100vw)";
        login.style.transition="all 1s ease";
        await esperar(500);
        login.classList.remove("on");
        login.classList.add("off");
        registro.style.transform="translateX(+100vw)";
        registro.classList.remove("off");
        registro.classList.add("on");
        await esperar(50);
        registro.style.transform="translateX(0px)";
        registro.style.transition="all 1s ease";


    }
    if(registro.classList.contains("on")&& click.id==="Rvoltar"){
        registro.style.transform="translateX(100vw)";
        registro.style.transition="all 1s ease";
        await esperar(500);
        registro.classList.remove("on");
        registro.classList.add("off");
        login.style.transform="translate(-100vw)"
        login.classList.remove("off");
        login.classList.add("on");
        await esperar(50);
        login.style.transform="translateX(0px)";
        login.style.transform="all 1s ease";
    }
})