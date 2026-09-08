function AtualizarTamanho(){
    const altura = window.innerHeight;
    const loginImage= window.document.querySelector("#loginimage");
    const login=window.document.querySelector("#login")
    const body = window.document.body;
    if(altura <300 && loginImage.classList.contains("active")&& login.classList.contains("active")){
        loginImage.classList.add("off");
        loginImage.classList.remove("active");
        
    }
    else if(altura >300 && loginImage.classList.contains("off")&& login.classList.contains("active"))
    {
        body.style.gridTemplateRows = "auto 190px auto"
        loginImage.classList.remove("off");
        loginImage.classList.add("active");
        body.style.gridTemplateRows = "auto 300px auto"
    }
        
}
function ClickSingUp(){
    const login = window.document.querySelector("#login");
    const loginImagem=window.document.querySelector("#loginimage")
    
    
    if(login.classList.contains("active")){
        
        login.style.transform="translateX(-500px)";
        login.style.zIndex = "1";
        login.style.transition="all 3s ease";
    }
}
AtualizarTamanho();

const signUp=window.document.querySelector("#btnrdiv");
window.addEventListener('resize',AtualizarTamanho);

signUp.addEventListener("click",
    ClickSingUp
)