class ShootingMethod {
	
	static getLineEquation(x1, y1, x2, y2) {
		var f = function (x, x1, y1, x2, y2) {
			return y1;
		}
		if (y1 == y2) return f;

		var linear = function(x, x1, y1, x2, y2) {
			return (x - x1) / (x2 - x1) * (y2 - y1) + y1;
		}
		return linear;
	}

	static getLastY(f, x0, y0, z0, xn, h) {
		
		var stepCount = ((xn - x0) / h);
		var xi = x0, yi = y0, zi = z0;
		for (var i = 1; i <= stepCount; i++)
		{
			var deltaY = h * zi;
			zi += h * f(xi, yi, zi);
			yi += deltaY;
			xi += h;
		}
		return yi;
	}
	
	static Solve(f, a, b, ya, yb, z1, z2, precision)
	{
		if (f == null){
			alert("функция не определена");
			return null;
		}
		if (b <= a) {
			alert("b должно быть больше a");
			return null;
		}

		var step = (b - a) / 3;//3 = stepCount
		var try1 = ShootingMethod.getLastY(f, a, ya, z1, b, step);
		var try2 = ShootingMethod.getLastY(f, a, ya, z2, b, step);

		var lineFunc = ShootingMethod.getLineEquation(try1, z1, try2, z2);

		var z = lineFunc(yb, try1, z1, try2, z2);
		return z;
	}
}