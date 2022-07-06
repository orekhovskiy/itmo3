class RungeKuttMethod
 {
 	static runge(x0, y0, end, accur, f) {
		var k1 = 0, k2 = 0, k3 = 0, k4 = 0;
		var step = accur;
		var n = ((end - x0) / step) + 1;

		var points = [[], []];

		points[0][0] = x0;
		points[1][0] = y0;
		for (var i = 1; i < n; i++) {
			k1 = f(x0, y0);
			k2 = f(x0 + step / 2, y0 + step * k1 /2);
			k3 = f(x0 + step / 2, y0 + step * k2 / 2);
			k4 = f(x0 + step, y0 + step * k3);

			x0 += step;
			y0 = y0 + step * (k1 + 2*k2 + 2*k3 + k4) / 6;

			points[0][i] = x0; 
			points[1][i] = y0;
		}
		return points;
    }
}