from fastapi import APIRouter

from numpy import matmul, random

router = APIRouter()


@router.get('')
def hello_world() -> dict:
    return {'msg': 'Hello, World!'}

@router.get('/matrix')
def processMatrix() -> dict:
    matrix_a = random.rand(10, 10)
    matrix_b = random.rand(10, 10)
    return {
        'matrix_a': matrix_a.tolist(),
        'matrix_b': matrix_b.tolist(),
        'product': matmul(matrix_a, matrix_b).tolist()
    }