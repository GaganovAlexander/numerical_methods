from utils import *


def first_var(func, a, x):
    return a - (calc(func, a)/(calc(func, x) - calc(func, a)))*(x-a)

def second_var(func, b, x):
    return x - (calc(func, x)/(calc(func, b) - calc(func, x)))*(b-x)

def chord_method(func, a, b, eps):
    if calc(func, a)*calc(func, b) >= 0:
        return
    second_der_a = calc(derivative(derivative(func)), a)
    second_der_b = calc(derivative(derivative(func)), b)
    if second_der_a * second_der_b < 0:
        return
    
    if second_der_a < 0:
        func = list(map(lambda x: -x, func))

    if calc(func, a) > 0:
        fixed, x = a, b
        next = first_var
    else:
        fixed, x = b, a
        next = second_var
    x_1 = None
    while x_1 is None or not simple_stop(x, x_1, eps):
        x_1 = x
        x = next(func, fixed, x)
    return x


print(res := chord_method(func := [1, -1, 0, 1], -2, -1, 0.001), calc(func, res))