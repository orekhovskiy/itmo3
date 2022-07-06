class NewtonMethod {
	static difference(x, h, k) {
		if (k == 0) return f(x);
		return ( NewtonMethod.difference(x + h, h, k - 1) -  NewtonMethod.difference(x, h, k - 1)) / (h * k);
	}

	static interpolate(x0, step, nodeCount) {
		if (nodeCount < 2) {
			alert("Количество узлов не может быть меньше 2.");
			return null;
		}

		if(Math.abs(step) < 0.1) {
			alert(Math.abs(step));
			alert("Шаг не может быть равен нулю.");
			return null;	
		}

		var xn = x0;
		for (var i = 0; i < nodeCount; i++) {
			var yn = f(xn);
			if (isNaN(yn) || !isFinite(yn)){
				alert("Функция должна быть определена во всех узлах.");
				return null;
			}
			xn += step;
		}

		var coefficients = new Array(nodeCount);
		for (var i = 0; i < nodeCount; i++){
			coefficients[i] = NewtonMethod.difference(x0, step, i);
		}
		return coefficients;
	}
}