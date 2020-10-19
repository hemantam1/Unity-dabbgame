//#pragma strict
//
//public function numberFormat(number)
//{
//    var temp = number;
//  //var tabUnits = [" million", " billion", " trillion", " quadrillion", " quintillion", " sextillion", " septillion", " octillion", " nonillion", " decillion", " undecillion", " duodecillion", " tredecillion", " quattuordecillion", " quindecillion", " sexdecillion", " septendecillion", " octodecillion", " novemdecillion", " vigintillion", " unvigintillion", " duovigintillion", " tresvigintillion", " quattuorvigintillion", " quinquavigintillion", " sesvigintillion", " septemvigintillion", " octovigintillion", " novemvigintillion", " trigintillion", " untrigintillion", " duotrigintillion", " duotrigintillion", " trestrigintillion", " quattuortrigintillion", " quinquatrigintillion", " sestrigintillion", " septentrigintillion", " octotrigintillion", " noventrigintillion", " quadragintillion"];
//  var tabUnits = ["K","M","B","T","aa","bb","cc","dd","ee","ff","gg","hh","ii","jj","kk","ll","mm","nn","oo","pp","qq","rr","ss","tt","uu","vv","ww","xx","yy","zz"];
//  var highnumber = false;
//  var bignumber = 1000;
//  var tabposition = -1;
//  var p_atomepersecond = true;
//    var unit;
//    if (number >= bignumber) {
//        highnumber = true;
//        while (number >= bignumber) { bignumber *= 1000; tabposition++; }
//        //while (number >= bignumber && tabposition < 4 ) { bignumber *= 1000; tabposition++; }
//        number /= (bignumber / 1000);
//        unit = tabUnits[tabposition];
//    } else unit = "";
//    if (unit == undefined) return temp.toExponential(2);
//    var toround = (highnumber == true) ? (p_atomepersecond == true) ? 100 : 100 : 10;
//    var res = Math.round(number * toround) / toround;
//    return [res.toLocaleString().replace(",", ".") + '' + unit];
//}
