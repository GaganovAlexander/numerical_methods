from random import randint


def foo(x):
    return (x - 1)*(x - 2)*(x - 3)

iterations = 0

def dichotomy(f, presicion, x1=randint(-100, 100), x2=randint(-100, 100)):
    global iterations
    while True:
        iterations += 1
        if f(x1) == 0:
            return x1
        if f(x2) == 0:
            return x2
        
        while f(x1)*f(x2) > 0:
            x1 = randint(-100, 100)
        
        x3 = (x1 + x2) / 2
        if f(x3)*f(x2) < 0:
            if abs(x3 - x1) < presicion:
                return x3
            x1 = x3  
        else:
            if abs(x3 - x2) < presicion:
                return x3
            x2 = x3        

print(dichotomy(f=foo, presicion=0.01, x1=0, x2=1.5), iterations)