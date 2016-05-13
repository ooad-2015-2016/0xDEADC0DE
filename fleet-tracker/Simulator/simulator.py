import sys, json, urllib.request
from threading import Timer

INTERVAL = 15
URL = 'http://fleet-tracker.com/tick/tracker-id/#lat#/#lon#' # Promijeniti na pravi URL

class RepeatedTimer(object): # http://stackoverflow.com/a/13151299
    def __init__(self, interval, function, *args, **kwargs):
        self._timer     = None
        self.interval   = interval
        self.function   = function
        self.args       = args
        self.kwargs     = kwargs
        self.is_running = False
        self.start()

    def _run(self):
        self.is_running = False
        self.start()
        self.function(*self.args, **self.kwargs)

    def start(self):
        if not self.is_running:
            self._timer = Timer(self.interval, self._run)
            self._timer.start()
            self.is_running = True

    def stop(self):
        self._timer.cancel()
        self.is_running = False

timer = None

def send_request(data, do_printing = False):
	lat, lon = data[0][0], data[0][1]
	data.pop(0) # Juric bi me retroaktivno oborio da vidi ovo
	url = URL.replace('#lat#', lat)
	url = url.replace('#lon#', lon)
	if (do_printing):
		print(lat, lon)
	try:
		with urllib.request.urlopen(url) as req:
			print('Made request ' + url)
	except KeyboardInterrupt:
		timer.stop()
		exit()
	except:
		print('Couldn\'t send request!')
		timer.stop()
		exit()
	if len(data) == 0:
		print('All data sent!')
		timer.stop()
		exit()

def main(): # Prvi command line parametar je file iz kojeg se cita, drugi moze biti -print za ispis svih koordinata
	data = []
	global timer
	with open(sys.argv[1]) as file_in:
		for line in file_in:
			line = line.strip().split(' ')
			data.append([line[0], line[1]])
	if '-print' in sys.argv:
		timer = RepeatedTimer(INTERVAL, send_request, data, True)
	else:
		timer = RepeatedTimer(INTERVAL, send_request, data)

if __name__ == '__main__':
	main()