function getPageHTML() {
   return "<html>" + $("html").html() + "</html>";
}

function goLinkV3(linkV3) {
   //CheckV4 context
   if (window.top.location.pathname.toLowerCase().startsWith("/v4/")) {
      window.top.location.hash = "/v3.html?URL=" + linkV3;
   }
   else {
      window.top["ContentFrame"].location.href = linkV3;
   }
}

function goLinkV4(linkV4) {
   //CheckV4 context
   if (window.top.location.pathname.toLowerCase().startsWith("/v4/")) {
      window.top.location.hash = linkV4;
   }
   else {
      window.top["ContentFrame"].location.href = "/v4/index.aspx#" + linkV4;
   }
}

function getLocation(href) {
   var location = document.createElement("a");
   location.href = href;
   // IE doesn't populate all link properties when setting .href with a relative URL,
   // however .href will return an absolute URL which then can be used on itself
   // to populate these additional fields.
   if (location.host == "") {
      location.href = location.href;
   }
   return location;
}

//-- COMPATIBILIDAD CON V3 --
if (window.top.location.pathname.toLowerCase().startsWith("/v4/")) {
   Object.defineProperty(window, "ContentFrame", {
      get: function () {
         for (var i = 0; i < window.frames.length; i++) {
            if (window.frames[i].frameElement.id == 'ContentFrame') {
               return window.frames[i];
            }
         }
      }
   });

   var topFrame = {
      winModalWindow: null,
      IE4: document.all,
      ShowModal: function (url, width, height, modal, funcOnClose) {
         if (width == null)
            width = 400;
         if (height == null)
            height = 200;
         if (modal == null)
            modal = 0;

         fnOnClose = funcOnClose;

         var posTop = (screen.availHeight) ? (screen.availHeight - height) / 2 : 50;
         var posLeft = (screen.availWidth) ? (screen.availWidth - width) / 2 : 50;

         if (url.indexOf('?') == -1)
            url += '?';
         else
            url += '&';
         url += 'zzz=' + (new Date()).getTime().toString();

         if (modal <= 1)
            modal = 2;

         switch (modal) {
            case 0:
               winModalWindow = window.showModelessDialog(url, 'ModalChild', 'dialogHeight: ' + height + 'px; dialogWidth: ' + width + 'px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No; unadorned: No');
               winModalWindow.parentFrame = top.ContentFrame;
               break;
            case 1:
               if ($.browser.msie)
                  winModalWindow = window.showModalDialog(url, 'ModalChild', 'dialogHeight: ' + height + 'px; dialogWidth: ' + width + 'px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No; unadorned: No');
               else {
                  ShowModalActivate();
                  winModalWindow = window.open(url, 'ModalChild', 'dependent,top=' + posTop + ',left=' + posLeft + ',scrollbars=1,titlebar=0,menubar=0,resizable=1,status=0,width=' + width + ',height=' + height);
                  winModalWindow.parentFrame = top.ContentFrame;
               }
               break;
            case 2:
               // usar jquery
               top.ContentFrame['CylModalOnClose'] = fnOnClose;
               this.cylModalShow(top.ContentFrame.document, url, width, height);
               break;
            case 3:
               winModalWindow = window.open(url, 'ModalChild', 'dependent,top=' + posTop + ',left=' + posLeft + ',scrollbars=1,titlebar=0,menubar=0,resizable=1,status=0,width=' + width + ',height=' + height);
               winModalWindow.parentFrame = top.ContentFrame;
         }
      },
      cylModalShow: function (targetDocument, url, width, height, fnOnClose) {
         if (width == null || width == '100%')
            width = '90%';
         else {
            width += 'px';
         }

         if (height == null || height == '100%')
            height = '85%';
         else {
            height += 'px';
         }

         targetDocument['CylModalOnClose'] = fnOnClose;

         $(targetDocument.body).append('<div id="modal-container"><div id="modal-overlay"></div>'
            + '<div id="modal-frame">'
            + '<div id="modal-close" style="display: none;">'
            + '<img src="../js/jquery/x.png" width="25" height="29" border="0" />'
            + '</div>'
            + '<iframe src="' + url + '" frameborder="0"></iframe>'
            + '</div></div>');

         $('#modal-frame', targetDocument).attr('defWidth', width);

         scrollTop = $('body', targetDocument).scrollTop();
         scrollLeft = $('body', targetDocument).scrollLeft();

         if (this.IsQuirksMode(targetDocument)) { // fix para IE en modo quirks (sin DOCTYPE)
            $('#modal-container', targetDocument).css({
               position: 'absolute',
               top: (scrollTop) + 'px',
               left: (scrollLeft) + 'px',
               height: '100%',
               width: '100%'
            });
         } else {
            $('#modal-container', targetDocument).css({
               position: 'fixed',
               top: '0px',
               left: '0px',
               height: '100%',
               width: '100%'
            });
         }

         $('#modal-overlay', targetDocument).css({
            display: 'none',
            position: 'absolute',
            top: '0px',
            left: '0px',
            bottom: '0px',
            right: '0px',
            width: '100%',
            height: '100%',
            //    right: '0px',
            //    bottom: '0px',
            background: 'black',
            zIndex: '1000',
            opacity: 0.6
         });

         $('#modal-close', targetDocument).css({
            display: 'block',
            position: 'absolute',
            right: '-12px',
            top: '-12px',
            zIndex: '1100'
         }).click(function () { topFrame.cylModalClose(targetDocument); });

         $('#modal-frame', targetDocument).css({
            display: 'none',
            position: 'absolute',
            border: '4px solid #CCC',
            background: 'white',
            padding: '0px',
            marginLeft: '-12px',
            height: height,
            width: width,
            zIndex: '1001'
         });

         //CENTRA EL MODAL EN EL FRAME
         this.centerModal(targetDocument.getElementById('modal-frame'));

         $('#modal-frame iframe', targetDocument).css({
            width: '100%',
            height: '100%'
         });

         $('#modal-overlay', targetDocument).fadeIn("fast", function () {
            $('#modal-frame', targetDocument).fadeIn("fast");
         });

         $("body", targetDocument).css("overflow", "hidden")[0].onselectstart = function () {
            return false;
         };
      },
      cylModalClose: function (targetDocument, doneFunction, modalResult) {
         $('#modal-frame', targetDocument).fadeOut("fast", function () {
            $('#modal-overlay', targetDocument).fadeOut("fast", function () {
               $('#modal-container', targetDocument).remove();
               //$(window).unbind('resize.cylModal');
               if (doneFunction) {
                  doneFunction(targetDocument, modalResult);
               }
            });
         });

         $("body", targetDocument).css("overflow", "auto")[0].onselectstart = function () { return true; };
      },
      IsQuirksMode: function (targetDocument) {
         return targetDocument.compatMode != 'CSS1Compat';
      },
      centerModal: function (elem) {
         var modalWidth = elem.style.width;
         var modalHeight = elem.style.height;
         elem.style.position = 'fixed';
         if (modalWidth.indexOf('%') > 0) {
            elem.style.left = (100 - modalWidth.replace('%', '')) / 2 + '%';
         } else {
            elem.style.left = '50%';
            elem.style.marginLeft = - modalWidth.replace('px', '') / 2 + 'px';
         }

         if (modalHeight.indexOf('%') > 0) {
            elem.style.top = (100 - modalHeight.replace('%', '')) / 2 + '%';
         } else {
            elem.style.top = '50%';
            elem.style.marginTop = - modalHeight.replace('px', '') / 2 + 'px';
         }
      },
      setTitle: function (title, image, showAdvancedPrint, backcount) { },
      newAlert: function (title, mess, icon, mods) {
         var a;
         (this.IE4) ? a = makeMsgBox(title, mess, icon, 0, 0, mods) : alert(mess);
      },
      newConfirm: function (title, mess, icon, defbut, mods) {
         if (this.IE4) {
            //icon = (icon==0) ? 0 : 2;
            icon = icon ? icon : 0;
            defbut = (defbut == 0) ? 0 : 1;
            retVal = makeMsgBox(title, mess, icon, 4, defbut, mods);
            retVal = (retVal == 6);
         }
         else {
            retVal = confirm(mess);
         }
         return retVal;
      },
      newPrompt: function newPrompt(title, mess, def) {
         retVal = (this.IE4) ? makeInputBox(title, mess, def) : prompt(mess, def);
         return retVal;
      },
      IEBox: function (title, mess, icon, buts, defbut, mods) {
         retVal = (this.IE4) ? makeMsgBox(title, mess, icon, buts, defbut, mods) : null;
         return retVal;
      },
      Dialogs: {
         Buttons: { Ok: 0, OkCancel: 1, AbortRetryIgnore: 2, YesNoCancel: 3, YesNo: 4, RetryCancel: 5 },
         Icon: { None: 0, Error: 1, Question: 2, Exclamation: 3, Information: 4 },
         DefaultButton: { First: 0, Second: 1, Third: 2 },
         ReturnValue: { Ok: 1, Cancel: 2, Abort: 3, Retry: 4, Ignore: 5, Yes: 6, No: 7 },
         Modal: { Yes: 0, No: 1 }
      }
   }
}
//-- FIN COMPATIBILIDAD CON V3 --