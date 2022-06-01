var handleOnResize = function () {
   //var elem = $('.main-container');
   var elems = $("div[data-auto-height]");
   //var body = $('body');
   //var window = $(window);
   var wHeight = window.innerHeight;
   var newHeight;
   var attr;
   var item;

   function getSiblingsHeight(item) {
      var sHeight = 0;
      item.siblings().each(function () {
         attr = $(this).attr("data-height-reference");
         if (typeof attr !== typeof undefined && attr !== false) {
            //console.log('siblings: ' + $(this).attr('name') + ' ' + $(this).height());
            sHeight += $(this).height();
         }
      });
      if (item.parent('div').length > 0) {
         sHeight += getSiblingsHeight(item.parent('div'));
      }
      return sHeight;
   }

   elems.each(function () {
      newHeight = 0;
      item = $(this);
      newHeight += getSiblingsHeight(item);
      item.css('min-height', (wHeight - newHeight) + 'px');
   });
}

jQuery(document).ready(function () {
   $(window).resize(function () {
      setTimeout(function () {
         handleOnResize();
      }, 50); // wait 50ms until window resize finishes.
   });
   $(window).resize();
});