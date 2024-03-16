import numpy as np


def solve_linear_eq(eq: np.ndarray[float]) -> float:
      return (eq[-1] - sum(eq[1:-1])) / eq[0]

def reverse_passage(step_augmented_matrix: np.ndarray[float]) -> np.ndarray[float]:
    x = np.array([], dtype='float64')
    for i in range(len(step_augmented_matrix)-1, -1 , -1):
        step_augmented_matrix[i, i+1:-1] *= x[::-1]
        x = np.append(x, solve_linear_eq(step_augmented_matrix[i, i:]))
    return x

def single_div_method(augmented_matrix: np.ndarray[float]) -> np.ndarray[float]:
    for i in range(len(augmented_matrix)):
            augmented_matrix[i, :] /= augmented_matrix[i, i]
            for j in range(i+1, len(augmented_matrix)):
                  augmented_matrix[j, i:] -= augmented_matrix[i, i:]*augmented_matrix[j, i]
    return reverse_passage(augmented_matrix)


def rectangle_method(augmented_matrix: np.ndarray[float]) -> np.ndarray[float]:
    len_ = len(augmented_matrix)
    for i in range(len_-1):
        augmented_matrix[i, :] /= augmented_matrix[i, i]
        for j in range(i+1, len_):
            for k in range(i+1, len_+1):
                augmented_matrix[j, k] -= augmented_matrix[j, i]*augmented_matrix[i, k]
            augmented_matrix[j, i] = 0
    augmented_matrix[-1, :] /= augmented_matrix[-1, -2]
    return reverse_passage(augmented_matrix)

example_1 = np.array([[5, 0, 1, 11],
                    [2, 6, -2, 8],
                    [-3, 2, 10, 6]],
                    dtype='float64')
example_2 = np.array([[2, 1, 4, 16],
                    [3, 2, 1, 10],
                    [1, 3, 3, 16]],
                    dtype='float64')

print("Дополненая матрица СЛУ:", example_1, "Решения по методу единственного деления:", sep='\n')
res = single_div_method(example_1.copy())[::-1]
for i in range(len(res)):
    print(f"x{i+1} = {res[i]:.3f}")
print("Решения по методу прямоугольника:")
res = rectangle_method(example_1.copy())[::-1]
for i in range(len(res)):
    print(f"x{i+1} = {res[i]:.3f}")
print('-'*20)
print("Дополненая матрица СЛУ:", example_2, "Решения по методу единственного деления:", sep='\n')
res = single_div_method(example_2.copy())[::-1]
for i in range(len(res)):
    print(f"x{i+1} = {res[i]:.3f}")
print("Решения по методу прямоугольника:")
res = rectangle_method(example_2.copy())[::-1]
for i in range(len(res)):
    print(f"x{i+1} = {res[i]:.3f}")