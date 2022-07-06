var canvas  = document.getElementById("canvas");
canvas.addEventListener("click", canvasClickEvent, false);

var context = canvas.getContext("2d");
context.font = "24px Arial";
context.textAlign = "center";

var canvasSize = 705, center = 353, cellSize = 70.5, dotRadius = 7;

function updateCanvas() {
    var r = document.forms["form"]["form:r_field"].value;
    drawArea(r);
}

function drawArea(r) {
    context.clearRect(0, 0, canvasSize, canvasSize);
    const areaSize = cellSize * r ^ 0;
    context.fillStyle = "#FFCC00";
    context.beginPath();
    context.moveTo(center + areaSize/2, center);
    context.lineTo(center, center);
    context.lineTo(center, center + areaSize);
    context.fill();

    context.moveTo(center, center);
    context.arc(center, center, areaSize/2, Math.PI, Math.PI/2, true);
    context.fill();
    context.closePath();
    context.fillRect(center, center - areaSize, areaSize, areaSize);

    context.fillStyle = "Black";
    drawAxis();
    if (r !== undefined) {
        drawCoordinates(r, areaSize);
        drawPoints(r);
    }
}

function drawAxis() {
    const oddLength = 11, evenLength = 19, thickness = 5, spacing = 30;
    var length, height, offset, even = true;
    var x = 0, y = 0;
    for (var i = 0; i <= 20; i++) {
        length = even ? evenLength : oddLength;
        offset = length / 2 ^ 0;
        even = !even;

        if (i === 10) y++;
        else context.fillRect(center - offset, y, length, thickness);
        y += thickness + spacing;
    }

    even = true;
    for (i = 0; i <= 20; i++) {
        height = even ? evenLength : oddLength;
        offset = height / 2 ^ 0;
        even = !even;

        if (i === 10) x++;
        else context.fillRect(x, center - offset, thickness, height);
        x += thickness + spacing;
    }

    offset = thickness / 2 ^ 0;
    context.fillRect(center - offset, 0, thickness, canvasSize);
    context.fillRect(0, center - offset, canvasSize, thickness);
}

function drawCoordinates(r, areaSize) {
    var offset = r === 5 ? 10 : 0;
    context.fillText((r * 0.5).toString(), center - areaSize / 2 ^ 0, center - 20);
    context.fillText(r, center + areaSize - offset, center - 20);

    context.fillText(r, center + 20, center - areaSize + 2 * offset);
    context.fillText(r, center - 20, center + areaSize);
    context.fillText((r * 0.5).toString(), center + areaSize / 2 ^ 0, center + 30);
}

function drawPoints(r) {
    $('tbody > tr').each(function(index, element) {
        var x = parseFloat(element.cells[0].innerHTML),
            y = parseFloat(element.cells[1].innerHTML);

        var hit = check(x, y, r);

        context.fillStyle = (hit  ? "Green" : "Red");
        context.beginPath();
        context.arc(center + x * cellSize ^ 0, center - y * cellSize ^ 0, dotRadius, 0, 2 * Math.PI);
        context.fill();
        context.closePath();
    });
}

function getMousePos(canvas, e) {
    var rect = canvas.getBoundingClientRect();
    return {
        x: e.clientX - rect.left,
        y: e.clientY - rect.top
    };
}

function canvasClickEvent(e) {
    var input = document.forms["form"]["form:r_field"].value;
    var r = parseInt(input);
    if (input.length === 1 && r >= 1 && r <= 5) {
        var coordinates = getMousePos(canvas, e);
        var x = (coordinates.x - center) / cellSize;
        var y = (center - coordinates.y) / cellSize;

        document.forms["form"]["form:x_field"].value = x;
        document.forms["form"]["form:y_field"].value = y;

        document.forms["form"]["form:submit"].click();
    }
    else alert("Unable to get coordinates");
}

function setXField() {
    var x = document.getElementById("form:x_menu_input").value;
    document.forms["form"]["form:x_field"].value = x;
}

function check(x, y, r) {

    function distance(a, b) {
        return Math.sqrt(Math.pow(a.x - b.x, 2) + Math.pow(a.y - b.y, 2));
    }

    function triangleArea(a, b, c) {
        var ab = distance(a, b), bc = distance(b, c), ca = distance(c, a), p = (ab + bc + ca) / 2;
        return Math.sqrt(p * (p - ab) * (p - bc) * (p - ca));
    }

    function doubleEquals(a, b) {
        const epsilon = 1E-5;
        return Math.abs(a - b) < epsilon;
    }

    var p = {x: x, y: y}, center = {x: 0, y: 0};

    //четверть окружности радиуса R/2 в III четверти
    if (p.x <= 0 && p.y <= 0 && distance(center, p) <= r/2)
        return true;

    //Квадрат R x R в I четверти
    if (p.x >= 0 && p.x <= r && p.y >= 0 && p.y <= r)
        return true;

    ///треугольник в IV четверти
    var a = {x: r/2, y: 0}, b = {x: 0, y: -r};
    var abc = triangleArea(a, b, center),
        abp = triangleArea(a, b, p),
        acp = triangleArea(a, p, center),
        bcp = triangleArea(p, b, center);

    return doubleEquals(abc, abp + acp + bcp);
}