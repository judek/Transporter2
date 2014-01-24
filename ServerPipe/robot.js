var found = 0;
var n = 1;
var total = 0;
var THRESHOLD = 10000;

while (total < THRESHOLD) {
    n += 1;
    for (var i = 2; i <= Math.sqrt(n); i++) {
        if (!(n % i == 0)) {
			total++;
            postMessage(found);
        }
        else {
            found++;
        }
    }
}