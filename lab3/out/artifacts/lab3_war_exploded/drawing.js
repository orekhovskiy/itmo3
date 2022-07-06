function getStrike(x, y, r) {
    var strike = false;
    if (x < 0) {
        if (y > 0) strike = false;
        else {
            if (x * x + y * y <= r * r / 4) strike = true;
            else strike = false;
        }
    }
    else {
        if (y > 0) {
            if (-x / 2 + r / 2 >= y) strike = true;
            else strike = false;
        }
        else {
            if (x <= r && y >= -r) strike = true;
            else strike = false;
        }
    }
    return strike;
}
function drawNew() {
    var canvas = document.getElementById("canv");
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, 300, 300);
        ctx.beginPath();
        ctx.moveTo(150, 0);
        ctx.lineTo(150, 300);
        ctx.stroke();
        ctx.moveTo(0, 150);
        ctx.lineTo(300, 150);
        ctx.stroke();
        for (var i = 30; i < 300; i += 30) {
            ctx.moveTo(145, i);
            ctx.lineTo(155, i);
            ctx.stroke();
            ctx.strokeText((300 - 2 * i) / 60, 160, i);
            ctx.moveTo(i, 145);
            ctx.lineTo(i, 155);
            ctx.stroke();
            ctx.strokeText((2 * i - 300) / 60, i, 140);
        }
        return ctx;
    }
}
function drawDefault() {
    document.getElementById('form:numberOfPoints').value = 0;
    var canvas = document.getElementById("canv");
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, 300, 300);
        ctx.beginPath();
        ctx.moveTo(150, 0);
        ctx.lineTo(150, 300);
        ctx.stroke();
        ctx.moveTo(0, 150);
        ctx.lineTo(300, 150);
        ctx.stroke();
        for (var i = 30; i < 300; i += 30) {
            ctx.moveTo(145, i);
            ctx.lineTo(155, i);
            ctx.stroke();
            ctx.strokeText((300 - 2 * i) / 60, 160, i);
            ctx.moveTo(i, 145);
            ctx.lineTo(i, 155);
            ctx.stroke();
            ctx.strokeText((2 * i - 300) / 60, i, 140);
        }
        canvas.addEventListener('click', function (evt) {
            var x = (evt.pageX - canvas.offsetLeft - 150) / 30;
            var y = (150 - evt.pageY + canvas.offsetTop ) / 30;
            var r = parseFloat(document.getElementById("form:rInput").value);
            y *= 100;
            y = Math.round(y);
            y /= 100;
            document.getElementById("form:xSlider").value = x;
            document.getElementById("form:yInput").value = y;
            var form = document.getElementById("form");
            drawPoint(x, y, getStrike(x, y, r));
            form.submit();
        }, false);
        return ctx;
    }
}
function getPoints(points){
    drawArea(document.getElementById("form:rInput"));
    var currentR = document.getElementById("form:rInput").value;
    for (var i = 0; i < points.length; i+=4){
        var x = parseFloat(points[i]);
        var y = parseFloat(points[i + 1]);
        var strike = getStrike(x, y, currentR);
        drawPoint(x, y, strike);
    }
    var currentX = parseFloat(document.getElementById("form:sliderValue").innerHTML);
    var currentY = parseFloat(document.getElementById("form:yInput").value);
    var strike = getStrike(currentX, currentY, currentR);
    drawPoint(currentX, currentY, strike);
}
function drawArea(rInput) {
    document.getElementById("form:j_idt10").innerHTML = "";
    var r = parseFloat(rInput.value);
    var canvas = document.getElementById("canv");
    if (!Number.isNaN(r) && r >= 2 && r <= 5) {
        var ctx = drawNew();
        ctx.beginPath();
        ctx.moveTo(150, 150);
        ctx.lineTo(150, 150 - r * 30 / 2);
        ctx.lineTo(150 + r * 30, 150);
        ctx.fillStyle = "rgba(0,0,255)";
        ctx.fill();
        ctx.closePath();
        ctx.fillRect(150, 150, r * 30, r * 30);
        ctx.beginPath();
        ctx.moveTo(150, 150);
        ctx.arc(150, 150, r * 15, Math.PI / 2, Math.PI);
        ctx.fill();
        ctx.closePath();
    }
    else {
        document.getElementById("form:j_idt10").innerHTML="Радиус должен быть числом в пределах [2;5]";
        drawDefault();
    }

}
function drawPoint(x, y, strike) {
    if (y < -5 || y > 3) return;
    var canvas = document.getElementById("canv");
    var ctx = canvas.getContext('2d');
    if (strike) ctx.fillStyle = "rgb(0,255,0)";
    else ctx.fillStyle = "rgb(255,0,0)";
    ctx.beginPath();
    ctx.moveTo(150, 150);
    ctx.arc(150 + x * 30, 150 - 30 * y, 3, 0, 2 * Math.PI, true);
    ctx.fill();
}