<%@ page import="java.util.ArrayList" %>

<html>

<head>
    <meta charset="UTF-8">
    <link href="style.css" rel="stylesheet">
    <title>Laboratory work #2</title>
</head>

<body>
<header id="index_header">
    <div class="logo"><img src="itmo_header.png" width="120px" height="60px" ></div>
    <div class="lab">Lab 2</div>
    <div class="authors">Chemyrtan Andrey, Orekhovskiy Anton P3217</div>
</header>

<img src="axis.png" id="axis" style="visibility: hidden; width: 0; height: 0">

<div class="outer" id="main_block">
  <div class="inner" id="canvas_block">
    <canvas id="canvas" width="500px" height="500px"> </canvas>
    <script>
        var canvas  = document.getElementById("canvas"),
            context = canvas.getContext("2d");
        context.font = "24px Arial";
        context.textAlign = "center";

        const center = 250, cellSize = 50, dotRadius = 2;

        function drawArea(r) {
            r = r * cellSize;
            const areaSize = cellSize * r ^ 0;
            context.clearRect(0, 0, 600, 600);
            drawAxis();
            var radiusDefined = typeof(r) !== "undefined";
            if (radiusDefined) {
                context.fillStyle = "rgb(0,0,255)";
                context.fillRect(250 - r / 2, 250, r / 2, r);
                context.beginPath();
                context.arc(250, 250, r, 0, 1.5 * Math.PI, true);
                context.lineTo(250, 250 + r / 2);
                context.fill();
                context.closePath();
            }
            drawPoints(r);
        }

        function drawAxis() {
            var axis = document.getElementById("axis");
            context.drawImage(axis,0,0, 500, 500);
        }
        function areaCheck(x,y,r) {
            if (Math.pow(y, 2) + Math.pow(x, 2) <= Math.pow(r, 2) && x >= 0 && y >= 0 ||
                y >= 0.5 * x - (r / 2) &&  x >= 0 && y <= 0 ||
                Math.abs(y) <= r && Math.abs(x) <= r / 2 && x <= 0 && y <=0)
            return true;
        else
            return false;
        }
        function drawPoints(r) {
            /*$('table > tbody  > tr').each(function(index, element) {*/
            var table = document.getElementById("table");
            for (var i = 1, row; row = table.rows[i]; i++) {
                var x = parseFloat(row.cells[1].innerHTML),
                    y = parseFloat(row.cells[2].innerHTML);
                var result = areaCheck(x, y, r / cellSize);
                context.fillStyle = (result ? "Green" : "Red");
                context.beginPath();
                context.arc(center + x * cellSize ^ 0, center - y * cellSize ^ 0, dotRadius, 0, 2 * Math.PI);
                context.fill();
                context.closePath();
            };
        }

        function updateCanvas() {
            var r = null;
            var input = document.getElementsByName("r_field");
            for (var i = 0; i < input.length; i++){
                if (input[i].checked){
                    r = input[i].value;
                    break;
                }
            }
            var radiusDefined = r >= 1 && r <= 5 && r!=null;
            drawArea(radiusDefined ? r : undefined);
        }

        function getMousePos(canvas, e) {
            var rect = canvas.getBoundingClientRect();
            return {
                x: e.clientX - rect.left,
                y: e.clientY - rect.top
            };
        }

        canvas.addEventListener("click", canvasClickEvent, false);
        function canvasClickEvent(e) {
            var r = null;
            var input = document.getElementsByName("r_field");
            for (var i = 0; i < input.length; i++){
                if (input[i].checked){
                    r = input[i].value;
                    break;
                }
            }
            if (r!=null && r >= 1 && r <= 5)
            {
                var coordinates = getMousePos(canvas, e);
                document.forms["form"]["x_field"][0].value = (coordinates.x - center) / cellSize;
                document.forms["form"]["y_field"].value = (center - coordinates.y) / cellSize;
                document.forms["form"].submit();
            }
            else alert("Unable to get coordinates")
        }
    </script>
  </div>

  <div class="inner" style="padding-left: 150px">
    <div id="validation_block">
      <form name="form" action="./check" onsubmit="return validate()" method="get">
        <p id="info_field">Input parameters:</p>
        Input radius:
          <input type="radio" name="r_field" value="1" onclick="updateCanvas();">1
          <input type="radio" name="r_field" value="2" onclick="updateCanvas();">2
          <input type="radio" name="r_field" value="3" onclick="updateCanvas();">3
          <input type="radio" name="r_field" value="4" onclick="updateCanvas();">4
          <input type="radio" name="r_field" value="5" onclick="updateCanvas();">5
          <br><br>
        Input coordinate X:
        <select name="x" id="x_field">
          <option value="-3">-3</option>
          <option value="-2">-2</option>
          <option value="-1">-1</option>
          <option value="0">0</option>
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
          <option value="5">5</option>
        </select>
          <br><br>
        Input coordinate Y:
        <input type="text" id="y_field" name="y">
        <br><br>
        <input type="submit" value="Send" class="button">
      </form>
      <script>
          function setInfoText(description) {
              document.getElementById("info_field").innerHTML = description;
              return false;
          }

          function validate() {
              const int_regex = /^0|-?[1-9]\d*$/,
                  real_regex = /^0|-?(?:(?:[1-9]\d*(?:[,.]\d+)?)|0[,.]\d+)$/;

              if (!int_regex.test(document.forms["form"]["r_field"].value))
                  return setInfoText("Incorrect input in radius field");

              if (!real_regex.test(document.forms["form"]["x_field"].value))
                  return setInfoText("Incorrect input in X field");

              if (!real_regex.test(document.forms["form"]["y_field"].value))
                  return setInfoText("Incorrect input in Y field");

              var r = parseInt(document.forms["form"]["r_field"].value);
              if (r < 1 || r > 5)  return setInfoText("Radius must be an integer between 1 and 5");

              var x = parseFloat(document.forms["form"]["x_field"].value);
              if (x < -5 || x > 5) return setInfoText("X must be between -5 and 5");

              var y = parseFloat(document.forms["form"]["y_field"].value);
              if (y < -5 || y > 5) return setInfoText("Y must be between -5 and 5");
          }
      </script>
    </div>
  </div>
</div>

<div>
  <table id="table">
    <thead>
    <tr>
      <th>Radius</th>
      <th>X</th>
      <th>Y</th>
      <th>Included?</th>
    </tr>
    </thead>
    <tbody>
    <%
      ServletContext context = session.getServletContext();
      try {
        ArrayList<Float> r = (ArrayList<Float>) context.getAttribute("r"),
                x = (ArrayList) context.getAttribute("x"),
                y = (ArrayList) context.getAttribute("y");
        ArrayList<Boolean> result = (ArrayList) context.getAttribute("result");

        int count = r.size();
        for (int i = 0; i < count; i++) {
          String resultStr = result.get(i) ? "Yes" : "No";
          out.println("<tr>" +
                  "   <td>" + r.get(i) + "</td>" +
                  "   <td>" + x.get(i) + "</td>" +
                  "   <td>" + y.get(i) + "</td>" +
                  "   <td class=\"" + resultStr + "Row\">" + resultStr + "</td>" +
                  "</tr>");
        }
      }
      catch (Exception e) {}
    %>
    </tbody>
  </table>
</div>
</body>
</html>