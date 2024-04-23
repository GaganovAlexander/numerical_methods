from utils import *


def newton(func, x0, eps, stop=simple_stop, x_1=None):
    der = calc(derivative(func), x0)
    if der == 0:
        der += eps
    x1 = x0 - calc(func, x0)/der
    if x_1 is not None and stop(x0, x1, eps, x_1):
        return x1
    return newton(func, x1, eps, stop, x0)

def simplified_newton(func, der, x0, eps, stop=simple_stop, x_1=None):
    x1 = x0 - calc(func, x0)/der
    if x_1 is not None and stop(x0, x1, eps, x_1):
        return x1
    return simplified_newton(func, der, x1, eps, stop, x0)

def newton_broiden(func, x0, c, eps, stop=simple_stop, x_1=None):
    der = calc(derivative(func), x0)
    if der == 0:
        der += eps
    x1 = x0 - c*(calc(func, x0)/der)
    if x_1 is not None and stop(x0, x1, eps, x_1):
        return x1
    return newton(func, x1, eps, stop, x0)

def secant(func, x0, delta, eps, stop=simple_stop, x_1=None):
    if x_1:
        der = (func(x0) - func(x_1)) / (x0 - x_1)
    else:
        der = num_derivative(func, x0, delta)
    if der == 0:
        der += eps
    x1 = x0 - func(x0)/der
    if x_1 is not None and stop(x0, x1, eps, x_1):
        return x1
    return secant(func, x1, delta, eps, stop, x0)

def func_(x):
    return -6 + 11*x - 6*(x**2) + x**3


FUNC = [-6, 11, -6, 1]
eps = 0.01
x0 = 10

print(newton(func=FUNC, x0=x0, eps=eps, stop=simple_stop))
print(simplified_newton(func=FUNC, der=191, x0=x0, eps=eps, stop=simple_stop))
print(simplified_newton(func=FUNC, der=191, x0=x0, eps=eps, stop=geom_stop))
print(newton_broiden(func=FUNC, x0=x0, c=1.1, eps=eps, stop=simple_stop))
print(secant(func_, x0=x0, delta=0.1, eps=eps, stop=simple_stop))