from utils import *


iters = 0
def sqrt_(a: float, x0: float, eps: float, stop=simple_stop, x_1=None):
    global iters
    iters += 1
    x1 = 0.5*(x0+a/x0)
    if not x_1 is None and stop(x0, x1, eps, x_1):
        return x1
    return sqrt_(a, x1, eps, stop, x0)
    

print(sqrt_(4, 10, 0.001), iters)