def theorem_1(func):
    A = max(abs(a) for a in func[:-2])
    B = max(abs(a) for a in func[1:])    
    
    r = 1 / (1 + B / abs(func[0])) 
    R = 1  + A / abs(func[-1]) 
    
    return r, R

def theorem_2(func):
    if func[-1] < 0:
        func = [-a for a in func]
        
    for i in range(len(func) - 1, 0, -1):
        if func[i] < 0:
            break
    else:
        return
    C = max(abs(a) for a in func if a < 0)

    return 1 + (C / func[-1]) ** (1 / (len(func)-1 - i)) 

def theorem_3(func):
    R = theorem_2(func)
    R_1 = theorem_2(list(reversed(func)))
    R_2 = theorem_2([-a for a in func])
    R_3 = theorem_2([-a for a in list(reversed(func))])
    
    return (1/R_1, R), (-R_2, 1/R_3)

func = [-3, -7, 8, -5, 2, 1]

print(theorem_1(func))  
print(theorem_2(func)) 
print(theorem_3(func))