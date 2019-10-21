// Sample UDF which returns sum of two values.
function GetString(arg1) {
    
    var obj = { prop: "" };

    //obj.prop = (/[^.]+/.exec(arg1)[0]);

    obj.prop = (/utm_medium=.*[?&]/.exec(arg1)[0]);

   // var url = 'http://xxx.domain.com';
   // obj.prop = (/[^.]+/.exec(url)[0]);
    //obj.prop = (/[^.]+/.exec(url)[0].substr(7));
    
    return obj
}


