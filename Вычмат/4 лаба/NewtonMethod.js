class NewtonMethod {
	static difference(x, y, i, k)
	{
		if (k == 0) return y[i];
		return (NewtonMethod.difference(x, y, i + 1, k - 1) - NewtonMethod.difference(x, y, i, k - 1)) / (x[k] - x[0]);
	}
	static interpolate( x, y)
	{
		var nodeCount = x.length;
		var step = x[1] - x[0];
		var coefficients = new Array(nodeCount);
		for (var i = 0; i < nodeCount; i++) {
			coefficients[i] = NewtonMethod.difference(x, y, 0, i);
		}
		return coefficients;
	}
}