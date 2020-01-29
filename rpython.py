from __future__ import print_function
from threading import Lock
 
_global_lock = Lock()
_old_print = print
 
def print(*a, **b):
	with _global_lock:
		_old_print(*a, **b)
