class  EulerMethod {
	static getY1(y0, z0, step) {
		return y0 + step * z0;
	}

	static getY2(f, x0, y0, z0, step) {
		var y1 = y0 + step * z0;
		var z1 = z0 + step * f(x0, y0, z0);
		var deltaY = step * z1;
		return y1 + deltaY;
	}

	static approximate(f, x0, y0, z0, xn, step) {
		var stepCount = ((xn - x0) / step);

		var points = [[],[]];
		points[0][0] = x0;
		points[1][0] = y0;
		var xi = x0, yi = y0, zi = z0;
		for (var i = 1; i <= stepCount; i++)
		{
			var deltaY = step * zi;
			zi += step * f(xi, yi, zi);
			yi += deltaY;
			xi += step;
			points[0][i] = xi;
			points[1][i] = yi;
		}

		return points;
	}

	static Solve(f, x0, y0, z0, xn, precision)
	{
		if (f == null){
			alert("функция не определена");
			return null;
		}
		if (xn <= x0) {
			alert("xn должен быть больше x0");
			return null;
		}

		var stepCount = 3;
		var step = (xn - x0) / stepCount;
		var error, yn, y2n;
		do
		{
			stepCount *= 2;
			if (stepCount >= 20) break;

			yn = EulerMethod.getY1(y0, z0, step);
			step /= 2;
			y2n = EulerMethod.getY2(f, x0, y0, z0, step);
			error = Math.abs(yn - y2n);
		} while (error >= precision);
		var points = [[],[]];
		points = EulerMethod.approximate(f, x0, y0, z0, xn, step);
		return points;
	}
}