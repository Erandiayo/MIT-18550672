/*! http://mths.be/placeholder v2.0.8 by @mathias */(function(e,t,n){function c(e){var t={},r=/^jQuery\d+$/;n.each(e.attributes,function(e,n){n.specified&&!r.test(n.name)&&(t[n.name]=n.value)});return t}function h(e,t){var r=this,i=n(r);if(r.value==i.attr("placeholder")&&i.hasClass("placeholder"))if(i.data("placeholder-password")){i=i.hide().next().show().attr("id",i.removeAttr("id").data("placeholder-id"));if(e===!0)return i[0].value=t;i.focus()}else{r.value="";i.removeClass("placeholder");r==d()&&r.select()}}function p(){var e,t=this,r=n(t),i=this.id;if(t.value==""){if(t.type=="password"){if(!r.data("placeholder-textinput")){try{e=r.clone().attr({type:"text"})}catch(s){e=n("<input>").attr(n.extend(c(this),{type:"text"}))}e.removeAttr("name").data({"placeholder-password":r,"placeholder-id":i}).bind("focus.placeholder",h);r.data({"placeholder-textinput":e,"placeholder-id":i}).before(e)}r=r.removeAttr("id").hide().prev().attr("id",i).show()}r.addClass("placeholder");r[0].value=r.attr("placeholder")}else r.removeClass("placeholder")}function d(){try{return t.activeElement}catch(e){}}var r=Object.prototype.toString.call(e.operamini)=="[object OperaMini]",i="placeholder"in t.createElement("input")&&!r,s="placeholder"in t.createElement("textarea")&&!r,o=n.fn,u=n.valHooks,a=n.propHooks,f,l;if(i&&s){l=o.placeholder=function(){return this};l.input=l.textarea=!0}else{l=o.placeholder=function(){var e=this;e.filter((i?"textarea":":input")+"[placeholder]").not(".placeholder").bind({"focus.placeholder":h,"blur.placeholder":p}).data("placeholder-enabled",!0).trigger("blur.placeholder");return e};l.input=i;l.textarea=s;f={get:function(e){var t=n(e),r=t.data("placeholder-password");return r?r[0].value:t.data("placeholder-enabled")&&t.hasClass("placeholder")?"":e.value},set:function(e,t){var r=n(e),i=r.data("placeholder-password");if(i)return i[0].value=t;if(!r.data("placeholder-enabled"))return e.value=t;if(t==""){e.value=t;e!=d()&&p.call(e)}else r.hasClass("placeholder")?h.call(e,!0,t)||(e.value=t):e.value=t;return r}};if(!i){u.input=f;a.value=f}if(!s){u.textarea=f;a.value=f}n(function(){n(t).delegate("form","submit.placeholder",function(){var e=n(".placeholder",this).each(h);setTimeout(function(){e.each(p)},10)})});n(e).bind("beforeunload.placeholder",function(){n(".placeholder").each(function(){this.value=""})})}})(this,document,jQuery);