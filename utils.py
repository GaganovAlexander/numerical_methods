def simple_stop(x0, x1, eps, *args, **kwargs):
    return abs(x1 - x0) < eps

def geom_stop(x0, x1, eps, x_1, *args, **kwargs):
    return abs((x1 - x0)/(1-(x1-x0)/(x0-x_1))) < eps

def calc(func, x):
    return sum(func[i]*(x**i) for i in range(len(func)))

def derivative(func: list, *args, **kwargs):
    return [func[i]*i for i in range(1, len(func))]

def num_derivative(func, x, delta, *args, **kwargs):
    return (func(x) - func(x - delta)) / delta