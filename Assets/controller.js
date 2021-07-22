
var intervalID = window.setInterval(myCallback, 10);
var angle = 0; //valor pra retornar
var direcao = 1;
var aux = 1;
var id = window.setInterval(progresso, 15);
var parou = false;
var parouOretorno = false;
var width = 1;  //valor pra retornar
var x = 0;
var y = 0;
function myCallback() {
    document.querySelector("#img").style.transform ='rotate(' + angle + 'deg)'; 
    angle += direcao;
    if (angle>=180||angle<=-1) {
        direcao = direcao*(-1);
    }
}
function stop() {
    window.app.sendMessageToScreen('interact')
    clearInterval(intervalID);
    if (parou) {
        parouOretorno = true;
        console.log(angle);
    }
    parou = true;
    if (parouOretorno) {
        clearInterval(id);
        console.log(width);
        console.log(angle);
        var radian = degrees_to_radians(angle)
        // [x, y] = [Math.cos(radian), Math.sin(radian)]
        x = Math.cos(radian);
        y = Math.sin(radian);
        console.log(x);
        console.log(y);
        window.app.sendMessageToScreen(
            {
                "action": 'interact', 
                "x": x, 
                "y": y, 
                "force":width 
            }
        );
    } 
}
function progresso() {
    if(parou){  
        var elem = document.getElementById("myBar");  
        if (width >= 100 || width <=-1) {
            aux = aux*(-1);
        }
        width += aux;
        elem.style.width = width + "%";
    }
}
function paraTudo() {
    clearInterval(id);
}

function degrees_to_radians(degrees)
{
  return degrees * (Math.PI/180);
}

