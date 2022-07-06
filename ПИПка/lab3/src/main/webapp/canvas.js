var canvas  = document.getElementById("canvas");
canvas.addEventListener("click", canvasClickEvent, false);

var context = canvas.getContext("2d");
context.font = "24px Arial";
context.textAlign = "center";

var canvasSize = 705, center = 353, cellSize = 70.5, dotRadius = 7;

function updateCanvas() {
    r =document.getElementById("form:r_menu").value;
    document.forms["form"]["form:r_field"].value = r;
    drawArea(r);
}

function drawArea(r) {
    document.forms["form"]["form:r_field"].value = r;
    context.clearRect(0, 0, canvasSize, canvasSize);
    const areaSize = cellSize * r;
    context.fillStyle = "#0000ff";
    context.beginPath();
    context.moveTo(center, center);
    context.arc(center, center, areaSize/2, Math.PI, Math.PI/2, true);
    context.lineTo(center + areaSize, center);
    context.lineTo(center, center);
    context.fill();
    context.closePath();
    context.fillRect(center - areaSize / 2, center - areaSize, areaSize / 2, areaSize);

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
    if (r >= 1 && r <= 5) {
        var coordinates = getMousePos(canvas, e);
        var x = (coordinates.x - center) / cellSize;
        var y = (center - coordinates.y) / cellSize;

        document.forms["form"]["form:x_field"].value = x;
        document.forms["form"]["form:y_field"].value = y;

        document.forms["form"]["form:submit"].click();
    }
    else alert("Unable to get coordinates");
}

function check(x, y, r) {
    if (Math.abs(x) <= r/2 && Math.abs(y) <= r && x<=0 && y >= 0 ||
        Math.pow(x, 2) + Math.pow(y, 2) <= Math.pow(r / 2, 2) && x <= 0 && y <= 0||
        y >= 0.5 * x - (r / 2) &&  x >= 0 && y <= 0)
    return(true);
    else
    return(false);
}