var gapi=window.gapi=window.gapi||{};gapi._bs=new Date().getTime();(function(){var k=void 0,n=!0,p=null,q=!1,aa=encodeURIComponent,r=window,ba=Object,s=document,t=String,ca=decodeURIComponent,da="appendChild",u="push",v="test",ea="exec",y="replace",fa="getElementById",z="concat",A="indexOf",ga="readyState",C="createElement",ha="firstChild",D="setAttribute",ia="getTime",ja="getElementsByTagName",G="length",I="split",J="location",K="style",ka="call",L="getAttribute",M="href",la="action",N="apply",O="parentNode",P="join",Q="toLowerCase";var R=r,S=s,ma=R[J],na=function(){},oa=/\[native code\]/,T=function(a,b,c){return a[b]=a[b]||c},pa=function(a){for(var b=0;b<this[G];b++)if(this[b]===a)return b;return-1},qa=/&/g,ra=/</g,sa=/>/g,ta=/"/g,ua=/'/g,va=function(a){return t(a)[y](qa,"&amp;")[y](ra,"&lt;")[y](sa,"&gt;")[y](ta,"&quot;")[y](ua,"&#39;")},U=function(){var a;if((a=ba.create)&&oa[v](a))a=a(p);else{a={};for(var b in a)a[b]=k}return a},V=function(a,b){return ba.prototype.hasOwnProperty[ka](a,b)},wa=function(a){if(oa[v](ba.keys))return ba.keys(a);
var b=[],c;for(c in a)V(a,c)&&b[u](c);return b},W=function(a,b){a=a||{};for(var c in a)V(a,c)&&(b[c]=a[c])},X=T(R,"gapi",{});var xa=function(a,b,c){var f=RegExp("([#].*&|[#])"+b+"=([^&#]*)","g");b=RegExp("([?#].*&|[?#])"+b+"=([^&#]*)","g");if(a=a&&(f[ea](a)||b[ea](a)))try{c=ca(a[2])}catch(d){}return c},ya=/^([^?#]*)(\?([^#]*))?(\#(.*))?$/,za=function(a){a=a.match(ya);var b=U();b.j=a[1];b.c=a[3]?[a[3]]:[];b.e=a[5]?[a[5]]:[];return b},Aa=function(a){return a.j+(0<a.c[G]?"?"+a.c[P]("&"):"")+(0<a.e[G]?"#"+a.e[P]("&"):"")},Ba=function(a){var b=[];if(a)for(var c in a)V(a,c)&&a[c]!=p&&b[u](aa(c)+"="+aa(a[c]));return b},Ca=function(a,
b,c){a=za(a);a.c[u][N](a.c,Ba(b));a.e[u][N](a.e,Ba(c));return Aa(a)};var Da=function(a,b,c){if(R[b+"EventListener"])R[b+"EventListener"]("message",a,q);else if(R[c+"tachEvent"])R[c+"tachEvent"]("onmessage",a)};var Y;Y=T(R,"___jsl",U());T(Y,"I",0);T(Y,"hel",10);var Ea=function(a){return!Y.dpo?xa(a,"jsh",Y.h):Y.h},Fa=function(a){return T(T(Y,"H",U()),a,U())};var Ga=T(Y,"perf",U()),Ha=T(Ga,"g",U()),Ka=T(Ga,"i",U());T(Ga,"r",[]);U();U();var La=function(a,b,c){var f=Ga.r;"function"===typeof f?f(a,b,c):f[u]([a,b,c])},Ma=function(a,b,c){Ha[a]=!b&&Ha[a]||c||(new Date)[ia]();La(a)},Oa=function(a,b,c){b&&0<b[G]&&(b=Na(b),c&&0<c[G]&&(b+="___"+Na(c)),28<b[G]&&(b=b.substr(0,28)+(b[G]-28)),c=b,b=T(Ka,"_p",U()),T(b,c,U())[a]=(new Date)[ia](),La(a,"_p",c))},Na=function(a){return a[P]("__")[y](/\./g,"_")[y](/\-/g,"_")[y](/\,/g,"_")};var Pa=U(),Qa=[],Z;Z={b:"callback",o:"sync",l:"config",d:"_c",g:"h",p:"platform",k:"jsl",TIMEOUT:"timeout",n:"ontimeout",t:"mh",q:"u"};Qa[u]([Z.k,function(a){for(var b in a)if(V(a,b)){var c=a[b];"object"==typeof c?Y[b]=T(Y,b,[])[z](c):T(Y,b,c)}if(b=a[Z.q])a=T(Y,"us",[]),a[u](b),(b=/^https:(.*)$/[ea](b))&&a[u]("http:"+b[1])}]);var Ra=decodeURI("%73cript");Pa.m=function(a){var b=Y.ms||"https://apis.google.com";a=a[0];if(!a||0<=a[A](".."))throw"Bad hint";return b+"/"+a[y](/^\//,"")};
var Sa=function(a){return a[P](",")[y](/\./g,"_")[y](/-/g,"_")},Ta=function(a,b){for(var c=[],f=0;f<a[G];++f){var d=a[f];d&&0>pa[ka](b,d)&&c[u](d)}return c},Ua=/^[\/_a-zA-Z0-9,.\-!:=]+$/,Va=/^https?:\/\/[^\/\?#]+\.google\.com(:\d+)?\/[^\?#]+$/,Wa=/\/cb=/g,Xa=/\/\//g,Ya=function(a){var b=S[C](Ra);b[D]("src",a);b.async="true";a=S[ja](Ra)[0];a[O].insertBefore(b,a)},$a=function(a,b){var c=b||{};"function"==typeof b&&(c={},c[Z.b]=b);var f=c,d=f&&f[Z.d];if(d)for(var e=0;e<Qa[G];e++){var g=Qa[e][0],j=Qa[e][1];
j&&V(d,g)&&j(d[g],a,f)}f=a?a[I](":"):[];if(!(d=c[Z.g]))if(d=Ea(ma[M]),!d)throw"Bad hint";e=d;g=T(Y,"ah",U());if(!g["::"]||!f[G])Za(f||[],c,e);else{d=[];for(j=p;j=f.shift();){var h=j[I]("."),h=g[j]||g[h[1]&&"ns:"+h[0]||""]||e,l=d[G]&&d[d[G]-1]||p,m=l;if(!l||l.hint!=h)m={hint:h,i:[]},d[u](m);m.i[u](j)}var w=d[G];if(1<w){var x=c[Z.b];x&&(c[Z.b]=function(){0==--w&&x()})}for(;f=d.shift();)Za(f.i,c,f.hint)}},Za=function(a,b,c){var f=a.sort();a=[];for(var d=k,e=0;e<f[G];e++){var g=f[e];g!=d&&a[u](g);d=g}a=
a||[];var j=b[Z.b],h=b[Z.l],d=b[Z.TIMEOUT],l=b[Z.n],m=p,w=q;if(d&&!l||!d&&l)throw"Timeout requires both the timeout parameter and ontimeout parameter to be set";var f=T(Fa(c),"r",[]).sort(),x=T(Fa(c),"L",[]).sort(),B=[][z](f),Ia=function(a,b){if(w)return 0;R.clearTimeout(m);x[u][N](x,E);var d=((X||{}).config||{}).update;d?d(h):h&&T(Y,"cu",[])[u](h);if(b){Oa("me0",a,B);try{ab(function(){var a;a=c===Ea(ma[M])?T(X,"_",U()):U();a=T(Fa(c),"_",a);b(a)})}finally{Oa("me1",a,B)}}j&&j();return 1};0<d&&(m=R.setTimeout(function(){w=
n;l()},d));var E=Ta(a,x);if(E[G]){var E=Ta(a,f),H=T(Y,"CP",[]),F=H[G];H[F]=function(a){if(!a)return 0;Oa("ml1",E,B);var b=function(){H[F]=p;return Ia(E,a)};if(0<F&&H[F-1])H[F]=b;else for(b();(b=H[++F])&&b(););};if(E[G]){var Ja="loaded_"+Y.I++;X[Ja]=function(a){H[F](a);X[Ja]=p};a=c[I](";");a=(d=Pa[a.shift()])&&d(a);if(!a)throw"Bad hint:"+c;d=a=a[y]("__features__",Sa(E))[y](/\/$/,"")+(f[G]?"/ed=1/exm="+Sa(f):"")+("/cb=gapi."+Ja);e=d.match(Xa);g=d.match(Wa);if(!g||!(1===g[G]&&Va[v](d)&&Ua[v](d)&&e&&
1===e[G]))throw"Bad URL "+a;f[u][N](f,E);Oa("ml0",E,B);b[Z.o]||R.___gapisync?(b=a,"loading"!=S[ga]?Ya(b):S.write("<"+Ra+' src="'+encodeURI(b)+'"></'+Ra+">")):Ya(a)}else H[F](na)}else Ia(E)};var ab=function(a){if(Y.hee&&0<Y.hel)try{return a()}catch(b){Y.hel--,$a("debug_error",function(){r.___jsl.hefn(b)})}else return a()};X.load=function(a,b){return ab(function(){return $a(a,b)})};var bb=function(a){var b=r.___jsl=r.___jsl||{};b[a]=b[a]||[];return b[a]},cb=function(a){var b=r.___jsl=r.___jsl||{};b.cfg=!a&&b.cfg||{};return b.cfg},db=function(a){return"object"===typeof a&&/\[native code\]/[v](a[u])},eb=function(a,b){if(b)for(var c in b)b.hasOwnProperty(c)&&(a[c]&&b[c]&&"object"===typeof a[c]&&"object"===typeof b[c]&&!db(a[c])&&!db(b[c])?eb(a[c],b[c]):b[c]&&"object"===typeof b[c]?(a[c]=db(b[c])?[]:{},eb(a[c],b[c])):a[c]=b[c])},$=function(a){if(!a)return cb();a=a[I]("/");for(var b=
cb(),c=0,f=a[G];b&&"object"===typeof b&&c<f;++c)b=b[a[c]];return c===a[G]&&b!==k?b:k};var fb=function(a){var b=s[C]("div"),c=s[C]("a");c.href=a;b[da](c);b.innerHTML=b.innerHTML;return b[ha][M]},gb=function(a){a=a||"canonical";for(var b=s[ja]("link"),c=0,f=b[G];c<f;c++){var d=b[c],e=d[L]("rel");if(e&&e[Q]()==a&&(d=d[L]("href")))if(d=fb(d))return d}return r[J][M]};var hb={allowtransparency:"true",frameborder:"0",hspace:"0",marginheight:"0",marginwidth:"0",scrolling:"no",style:"",tabindex:"0",vspace:"0",width:"100%"},ib=0,jb=function(a,b,c){var f;try{f=a[C]('<iframe frameborder="'+va(c.frameborder)+'" scrolling="'+va(c.scrolling)+'" name="'+va(c.name)+'"/>')}catch(d){f=a[C]("iframe")}for(var e in c)a=c[e],"style"==e&&"object"===typeof a?W(a,f[K]):f[D](e,c[e]);for(;b[ha];)b.removeChild(b[ha]);b[da](f);c.allowtransparency&&(f.allowTransparency=n);return f};var kb=jb,jb=function(a,b,c,f,d,e){if((e||{}).allowPost&&2E3<f[G]){e=za(f);c.src="";c["data-postorigin"]=e.j;c=kb(a,b,c,f,d);var g;if(-1!=navigator.userAgent[A]("WebKit")){g=b[ha].contentWindow.document;g.open();f=g[C]("div");var j={},h=d+"_inner";j.name=h;j.src="";j.style="display:none";kb(a,f,j,"",h)}j=(f=e.c[0])?f[I]("&"):[];f=[];for(h=0;h<j[G];h++){var l=j[h][I]("=",2);f[u]([ca(l[0]),ca(l[1])])}e.c=[];j=Aa(e);e=a[C]("form");e.action=j;e.method="POST";e.target=d;e[K].display="none";for(d=0;d<f[G];d++)j=
a[C]("input"),j.type="hidden",j.name=f[d][0],j.value=f[d][1],e[da](j);b[da](e);e.submit();e[O].removeChild(e);g&&g.close();a=c}else a=kb(a,b,c,f,d);return a};var lb=/:([a-zA-Z_]+):/g,mb={style:"position:absolute;top:-10000px;width:300px;margin:0px;borderStyle:none"},nb="onPlusOne _ready _close,_open _resizeMe _renderstart oncircled".split(" "),ob=p,pb=T(Y,"WI",U()),qb=function(){var a=$("googleapis.config/sessionIndex");a==p&&(a=r.__X_GOOG_AUTHUSER);if(a==p){var b=r.google;b&&(a=b.authuser)}a==p&&(a=k,a==p&&(a=r[J][M]),a=a?xa(a,"authuser")||p:p);return a==p?p:t(a)},rb=function(a,b){if(!ob){var c=$("iframes/:socialhost:"),f=qb()||"0",d=qb();ob={socialhost:c,
session_index:f,session_prefix:d!==k&&d!==p&&""!==d?"u/"+d+"/":""}}return ob[b]||""},ub=function(a,b,c,f){if(!b[O])return p;if(!f){f=U();for(var d=0!=b.nodeName[Q]()[A]("g:"),e=0,g=b.attributes[G];e<g;e++){var j=b.attributes[e],h=j.name,j=j.value;0<=pa[ka](sb,h)||(d&&0!=h[A]("data-")||"null"===j)||(d&&(h=h.substr(5)),f[h[Q]()]=j)}d=b[K];(e=tb(d&&d.height))&&(f.height=t(e));(d=tb(d&&d.width))&&(f.width=t(d))}d=e=a;"plus"==a&&f[la]&&(e=a+"_"+f[la],d=a+"/"+f[la]);(e=$("iframes/"+e+"/url"))||(e=":socialhost:/_/widget/render/"+
d);d=e[y](lb,rb);e={};W(f,e);e.hl=$("lang")||"en-US";e.origin=r[J].origin||r[J].protocol+"//"+r[J].host;"plus"===a&&(g=e[M]?fb(e[M]):gb(f[la]?p:"publisher"),e.url=g,delete e[M]);"plusone"===a&&(e.url=f[M]?fb(f[M]):gb(),g=f.db,h=$(),g==p&&h&&(g=h.db,g==p&&(g=h.gwidget&&h.gwidget.db)),e.db=g||k,g=f.ecp,h=$(),g==p&&h&&(g=h.ecp,g==p&&(g=h.gwidget&&h.gwidget.ecp)),e.ecp=g||k,delete e[M]);e.hl=$("lang")||"en-US";Y.ILI&&(e.iloader="1");delete e["data-onload"];delete e.rd;g=$("inline/css");"undefined"!==
typeof g&&(0<c&&g>=c)&&(e.ic="1");g=/^#|^fr-/;c={};for(var l in e)V(e,l)&&g[v](l)&&(c[l[y](g,"")]=e[l],delete e[l]);l=[][z](nb);g=$("iframes/"+a+"/methods");"object"===typeof g&&oa[v](g[u])&&(l=l[z](g));for(var m in f)if(V(f,m)&&/^on/[v](m)&&("plus"!=a||"onconnect"!=m))l[u](m),delete e[m];delete e.callback;c._methods=l[P](",");d=Ca(d,e,c);f.rd?m=b:(m=s[C]("div"),b[D]("data-gapistub",n),m[K].cssText="position:absolute;width:100px;left:-10000px;",b[O].insertBefore(m,b));m.id||(b=m,T(pb,a,0),l="___"+
a+"_"+pb[a]++,b.id=l);b=U();b[">type"]=a;W(f,b);m[D]("data-gwattr",Ba(b)[P](":"));var w;l=d;a=m;b={allowPost:1,attributes:mb};m=a.ownerDocument;e=0;do c=b.id||["I",ib++,"_",(new Date)[ia]()][P]("");while(m[fa](c)&&5>++e);if(!(5>e))throw Error("Error creating iframe id");g=m[J][M];e=U();(h=xa(g,"_bsh",Y.bsh))&&(e._bsh=h);(g=Ea(g))&&(e.jsh=g);g=U();g.id=c;g.parent=m[J].protocol+"//"+m[J].host;h=xa(m[J][M],"id","");j=xa(m[J][M],"pfname","");(h=h?j+"/"+h:"")&&(g.pfname=h);b.hintInFragment?W(e,g):w=e;
w=Ca(l,w,g);l=U();W(hb,l);l.name=l.id=c;W(b.attributes,l);l.src=w;w=jb(m,a,l,w,c,b);a={};a.userParams=f;a.url=d;a.iframeNode=w;a.id=w[L]("id");return a},sb=["style","data-gapiscan"],tb=function(a){var b=k;"number"===typeof a?b=a:"string"===typeof a&&(b=parseInt(a,10));return b},vb=function(){};var wb,xb,yb,zb,Ab=/(?:^|\s)g-((\S)*)(?:$|\s)/,Bb={div:n,span:n},Cb=U();wb=T(Y,"SW",U());xb=T(Y,"SA",U());yb=T(Y,"FW",[]);zb=p;
var Eb=function(a,b){Db(k,q,a,b)},Db=function(a,b,c,f){Ma("ps0",n);c=("string"===typeof c?s[fa](c):c)||S;var d;d=S.documentMode;if(c.querySelectorAll&&(!d||8<d)){d=f?[f]:wa(wb)[z](wa(xb))[z](wa(Cb));for(var e=[],g=0;g<d[G];g++){var j=d[g];e[u](".g-"+j,"g\\:"+j)}d=c.querySelectorAll(e[P](","))}else d=c[ja]("*");c=U();for(e=0;e<d[G];e++){g=d[e];var h=g,j=f,l=h.nodeName[Q](),m=k;h[L]("data-gapiscan")?j=p:(0==l[A]("g:")?m=l.substr(2):(h=(h=t(h.className||h[L]("class")))&&Ab[ea](h))&&(m=h[1]),j=m&&(wb[m]||
xb[m]||Cb[m])&&(!j||m===j)?m:p);j&&(g[D]("data-gapiscan",n),T(c,j,[])[u](g))}if(b)for(var w in c){b=c[w];for(f=0;f<b[G];f++)b[f][D]("data-onload",n)}for(var x in c)yb[u](x);Ma("ps1",n);((w=yb[P](":"))||a)&&X.load(w,a);if(Fb(zb||{}))for(var B in c){a=c[B];x=0;for(b=a[G];x<b;x++)a[x].removeAttribute("data-gapiscan");Gb(B)}else{f=[];for(B in c){a=c[B];x=0;for(b=a[G];x<b;x++){d=a[x];if(xb[B])e=1;else{if(e=Cb[B])if(e=!!Bb[d.nodeName[Q]()])e=(e=d.innerHTML)&&e[y](/^[\s\xa0]+|[\s\xa0]+$/g,"")?n:q;e=e?0:
2}switch(e){case 0:Hb(B+"_annotation",d);break;case 1:Hb(B,d);break;case 2:if(e=B,g=f,d=ub(e,d,b))(j=d.id)&&g[u](j),Gb(e,d)}}}Ib(w,f)}},Jb=function(a){var b=T(X,a,{});b.go||(b.go=function(b){return Eb(b,a)},b.render=function(b,f){var d=f||{};d.type=a;var e=d,d=e.type;delete e.type;if(!d||!wb[d])throw Error("Unsupported widget "+d||"");var g=("string"===typeof b?s[fa](b):b)||k;if(g&&1===g.nodeType){var j={},h;for(h in e)V(e,h)&&(j[h[Q]()]=e[h]);e=j;e.rd=1;h=ub(d,g,0,e);Gb(d,h);(h=h.id)&&Ib(d,[h])}})};var Gb=function(a,b){var c=T(Y,"watt",U())[a];b&&c?c(b):X.load(a,function(){var c=T(Y,"watt",U())[a],d=b&&b.iframeNode;!d||!c?(0,X[a].go)(d&&d[O]):c(b)})},Fb=function(){return q},Ib=function(){},Hb=function(a,b){var c={};c.iframeNode=b;Gb(a,c)};T(X,"platform",{}).go=Eb;Fb=function(a){for(var b=[Z.d,Z.k,Z.g],c=0;c<b[G]&&a;c++)a=a[b[c]];b=Ea(ma[M]);return!a||0!=a[A]("n;")&&0!=b[A]("n;")&&a!==b};Ib=function(a,b){var c=function(){Da(f,"remove","de")},f=function(e){var f=e.data,j=e.origin;if(Kb(f,b)){var h=d;d=q;h&&Ma("rqe");$a(a,function(){h&&Ma("rqd");c();for(var a=T(Y,"RPMQ",[]),b=0;b<a[G];b++)a[b]({data:f,origin:j})})}};if(!(0===b[G]||!r.JSON||!r.JSON.parse)){var d=n;Da(f,"add","at");$a(a,c)}};
Qa[u]([Z.p,function(a,b,c){zb=c;b&&yb[u](b);for(b=0;b<a[G];b++)wb[a[b]]=n;var f=c[Z.d].annotation||[];for(b=0;b<f[G];++b)xb[f[b]]=n;f=c[Z.d].bimodal||[];for(b=0;b<f[G];++b)Cb[f[b]]=n;for(b=0;b<a[G];b++)Jb(a[b]);if(b=r.__GOOGLEAPIS)b.googleapis&&!b["googleapis.config"]&&(b["googleapis.config"]=b.googleapis),T(Y,"ci",[])[u](b),r.__GOOGLEAPIS=k;cb(n);f=r.___gcfg;b=bb("cu");if(f&&f!==r.___gu){var d={};eb(d,f);b[u](d);r.___gu=f}var f=bb("cu"),e=s.scripts||s[ja]("script")||[],d=[],g=[];g[u][N](g,T(Y,"us",
[]));for(var j=0;j<e[G];++j)for(var h=e[j],l=0;l<g[G];++l)h.src&&0==h.src[A](g[l])&&d[u](h);0==d[G]&&e[e[G]-1].src&&d[u](e[e[G]-1]);for(e=0;e<d[G];++e)if(!d[e][L]("gapi_processed")){d[e][D]("gapi_processed",n);(g=d[e])?(j=g.nodeType,g=3==j||4==j?g.nodeValue:g.textContent||g.innerText||g.innerHTML||""):g=k;if(g){for(;0==g.charCodeAt(g[G]-1);)g=g.substring(0,g[G]-1);j=k;try{j=(new Function("return ("+g+"\n)"))()}catch(m){}if("object"===typeof j)g=j;else{try{j=(new Function("return ({"+g+"\n})"))()}catch(w){}g=
"object"===typeof j?j:{}}}else g=k;g&&f[u](g)}e=bb("cd");f=0;for(d=e[G];f<d;++f)eb(cb(),e[f]);e=bb("ci");f=0;for(d=e[G];f<d;++f)eb(cb(),e[f]);f=0;for(d=b[G];f<d;++f)eb(cb(),b[f]);if("explicit"!=$("parsetags")){b=T(Y,"sws",[]);b[u][N](b,a);var x;if(c){var B=c[Z.b];B&&(x=function(){R.setTimeout(B,0)},delete c[Z.b])}if("complete"!==S[ga])try{Db(k,n)}catch(Ia){}var E=function(){Db(x,n)};if("complete"===S[ga])E();else{var H=q,F=function(){if(!H)return H=n,E[N](this,arguments)};R.addEventListener?(R.addEventListener("load",
F,q),R.addEventListener("DOMContentLoaded",F,q)):R.attachEvent&&(R.attachEvent("onreadystatechange",function(){"complete"===S[ga]&&F[N](this,arguments)}),R.attachEvent("onload",F))}}}]);var Lb=/^\{h\:'/,Mb=/^!_/,Kb=function(a,b){a=t(a);if(Lb[v](a))return n;a=a[y](Mb,"");if(!/^\{/[v](a))return q;try{var c=r.JSON.parse(a)}catch(f){return q}if(!c)return q;var d=c.f;return c.s&&d&&-1!=pa[ka](b,d)?("_renderstart"===c.s&&(c=c.a&&c.a[1],d=S[fa](d),c&&d&&vb(d[O],d,c)),n):q};vb=function(a,b,c){if(c.width&&c.height){a[K].cssText="";var f=c.width;c=c.height;var d=a[K];d.textIndent="0";d.margin="0";d.padding="0";d.background="transparent";d.borderStyle="none";d.cssFloat="none";d.styleFloat="none";d.lineHeight="normal";d.fontSize="1px";d.verticalAlign="baseline";a[K].display="inline-block";a=b[K];a.position="static";a.left=0;a.top=0;a.visibility="visible";f&&(a.width=f+"px");c&&(a.height=c+"px");b["data-csi-wdt"]=(new Date)[ia]()}};Ma("bs0",n,r.gapi._bs);Ma("bs1",n);delete r.gapi._bs;})();
gapi.load("plusone",{callback:window["gapi_onload"],_c:{"jsl":{"ci":{"services":{},"lexps":[17,69,84,71,80,82,61,74,75,30,45],"inline":{"css":1},"report":{},"oauth-flow":{},"isPlusUser":false,"iframes":{"additnow":{"url":"https://apis.google.com/additnow/additnow.html?bsv\u003dm"},"savetowallet":{"url":"https://clients5.google.com/s2w/o/savetowallet?bsv\u003dm"},"plus":{"methods":["onauth"],"url":":socialhost:/u/:session_index:/_/pages/badge?bsv\u003dm"},":socialhost:":"https://plusone.google.com","plus_circle":{"params":{"url":""},"url":":socialhost:/:session_prefix:_/widget/plus/circle?bsv\u003dm"},"evwidget":{"params":{"url":""},"url":":socialhost:/:session_prefix:_/events/widget?bsv\u003dm"},":signuphost:":"https://plus.google.com","plus_followers":{"params":{"url":""},"url":":socialhost:/_/im/_/widget/render/plus/followers?bsv\u003dm"},"plusone":{"preloadUrl":["https://ssl.gstatic.com/s2/oz/images/stars/po/Publisher/sprite4-a67f741843ffc4220554c34bd01bb0bb.png"],"params":{"count":"","size":"","url":""},"url":":socialhost:/:session_prefix:_/+1/fastbutton?bsv\u003dm"},"plus_share":{"params":{"url":""},"url":":socialhost:/:session_prefix:_/+1/sharebutton?plusShare\u003dtrue\u0026bsv\u003dm"}},"debug":{"host":"https://plusone.google.com","reportExceptionRate":0.05,"rethrowException":true},"csi":{"rate":0.0},"googleapis.config":{"mobilesignupurl":"https://m.google.com/app/plus/oob?"}},"h":"m;/_/apps-static/_/js/gapi/__features__/rt\u003dj/ver\u003d_RjRkrMuOa4.en./sv\u003d1/am\u003d!R7JhevK68w2IwTSFZw/d\u003d1/rs\u003dAItRSTOMThV6JeXW2C2otOqoovvCjFILVQ","u":"https://apis.google.com/js/plusone.js","hee":true,"fp":"9e85cd6b3771b8133eec0cdc405e6424565766c2","dpo":false},"platform":["plusone","plus","additnow","savetowallet"],"fp":"9e85cd6b3771b8133eec0cdc405e6424565766c2","annotation":[],"bimodal":[]}});