var utils = {
    
    isDom : ( typeof HTMLElement === 'object' ) ?
        function(obj){
            return obj instanceof HTMLElement;
        } :
        function(obj){
            return obj && typeof obj === 'object' && obj.nodeType === 1 && typeof obj.nodeName === 'string';
        } ,
        
    isArray : function(obj){
        return obj.constructor.toString().indexOf('Array') != -1;
    }
    
}
;(function(win){
    
    var jDom = function(selector){
        var jDom = null;
        var length = 0;
        var children = [];
        if(!selector) return;
        
        /** 1. 传入的是id * */
        if(selector.toString().indexOf('#') != -1) {
            selector = selector.replace('#','');
            jDom = document.getElementById(selector);
        } 
        
        /** 2. 传入的是class * */
        else if(selector.toString().indexOf('.') != -1){
            selector = selector.replace('.','');
            jDom = document.getElementsByClassName(selector);
        }
        
        
        /** 3. 传入的是dom元素 * */
        else if(utils.isDom(selector)){
            jDom = selector;
        }
        
         /** 4. 传入的是标签 * */
        else if(typeof selector === 'string'){
            jDom = document.getElementsByTagName(selector);
            return jDom;
        }
        
        if(!jDom) return; //如果本类库包装不了，就返回
        
        if(jDom.length){   //如果是一个类数组元素的话，就获取他的长度
            length = jDom.length; 
        }else{
            length = 1; //这种情况，说明成功包裹了元素，但是该元素还是存在的，就将长度设定为1
        }
        
        children = jDom.children; //取得所有的孩子节点
        
        return {
            
            /** 属性区 */
            obj : jDom,    //返回的dom元素
            index : 0 ,         //默认的角标（假如 jDom 是一个类数组的话）
            length : length,    //元素的个数（假如 jDom 是一个类数组的话）
            children : children,//所有孩子节点
            
            
            /** 方法区 */
            
            // ------------------------ dom 相关 ---------------------------------//
            
            /**获取dom对象本身,返回纯粹的dom元素，而非jDom元素*/
            getObj : function(){
                return this.obj;
            } ,
            
            /**获取元素的长度*/
            size : function(){
                return this.length;
            } ,
            
            /** 假如 jDom 是一个类数组的话，用于返回其中一个元素 */
            eq : function(index){
                if(length > 0) {
                    return $(this.obj[index]); //eq返回的还是jDom对象
                }else{
                    return null;
                }
            } ,
            
            /** 获得第一个匹配元素 */
            first : function(){
                return $(this.obj[0]);
            } ,
            
            /** 获得最后一个匹配元素 */
            last : function(){
                return $(this.obj[this.length - 1]);
            } ,
            
            /** 获得最后一个匹配元素 */
            getChildren : function(){
                return this.obj.children;
            } ,
            
            /** 获得某一个孩子节点 */
            getChild : function(i){
                return $(this.children[i]);
            } ,
            
            /** 获得父节点 */
            getParent : function(){
                return $(this.obj.parentElement);
            } ,
            
            /** 获得上一个节点 */
            previous : function(){
                var parent = this.getParent();
                var children = parent.children;
                for(var i = 0; i < children.length; i++){
                    if(this.obj == children[i]) {
                        return $(children[i - 1]);
                    }
                }
                return null;
            } ,
            
            /** 获得下一个节点 */
            next : function(){
                var parent = this.getParent();
                var children = parent.children;
                for(var i = 0; i < children.length; i++){
                    if(this.obj == children[i]) {
                        return $(children[i + 1]);
                    }
                }
                return null;
            } ,
            
            findClassDom : function(className){
                 this.obj = this.obj.getElementsByClassName(className) ;
                 return this ;
            } ,
            
            findIdDom : function(id){
                 var $this = this; 
                 var children = this.getChildren();
                 children = Array.prototype.slice.call(children); //obj 转  []
                 children.forEach(function(item){
                    //console.log(item.id);
                    (id === item.id) && ($this = item) ;
                 });
                 return this ;
            } ,
            
            // ------------------------ css 相关 ---------------------------------//
            /** 添加背景色 */
            backgroundColor : function(color){
                this.obj.style.backgroundColor = color;
                return this;
            } ,
            
            /** 获取style */
            getStyle : function(){
                var styleEle = null;
                if(window.getComputedStyle){
                    styleEle = window.getComputedStyle(this.obj,null);
                }else{
                    styleEle = ht.currentStyle;
                }
                return styleEle;
             } ,
            
            /** 设置或者拿到高度 */ 
            height : function(h){
                if(!h) return this.getStyle().getPropertyValue('height');
                (typeof h == 'number') && (h = h + 'px');
                this.obj.style.height = h;
                return this;
            } ,
            
            /** 设置或者拿到宽度 */ 
            width : function(w){
                if(!w) return this.getStyle().getPropertyValue('width');
                (typeof w == 'number') && (w = w + 'px');
                this.obj.style.width = w;
                return this;
            } ,
            
            /** 设置自定义样式 */
            css : function(obj){
                if(!obj) return;
                for(var key in obj){
                    //console.log(key + '=========' + obj[key]);
                    this.obj.style[key] = typeof obj[key] === 'number' ? obj[key] + 'px' : obj[key];
                }
                return this;
            } ,
            
            /** 设置放大 倍数*/
            scale : function(scaleNumber){
                this.css({
                    scale : scaleNumber
                });
                return this;
            } ,
            
            
            hasClass : function(cls) {  
                return this.obj.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));  
            }  ,
            
            addClass : function(cls){
                if (!this.hasClass(cls)) this.obj.className += " " + cls;  
            } ,
            
            removeClass : function(cls) {  
                if (this.hasClass(cls)) {  
                    //console.log(this.obj);
                    var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');  
                    this.obj.className = this.obj.className.replace(reg, ' ');  //修正bug，之前右边少了一个this
                }  
            } ,
            
            toggleClass : function(cls){  
                if(this.hasClass(cls)){  
                    this.removeClass(cls);  
                }else{  
                    this.addClass(cls);  
                }  
            }  ,
            
            
            
            // ------------------------ 动画 相关 ---------------------------------//
            
            //TODO
            animate : function(){
                
            } ,
            
            // ------------------------ 事件相关 ---------------------------------//
            
            on : function(eventName,callback){
                var $this = this;
                this.obj['on' + eventName] = function(){
                    callback.call($this,$this.obj); //context指向$this，参数传入dom对象
                };
                return this;
            } ,
            
            // ------------------------ 属性相关 ---------------------------------//
            
            attr : function(attr){
                return this.obj.attributes[attr];
            } ,
            
            
            // ------------------------ ajax相关 ---------------------------------//
            
            
            
            // ------------------------ ui ---------------------------------//
            
            /** 按钮 * */
            linkbutton : function(opts){
                var opts = opts || {};
                /**添加基本样式* */
                this.addClass('linkbutton');
                this.on('mouseover' , function(e){
                    //console.log(e);
                    this.css({
                        backgroundColor: '#d4ef50'
                    });
                }).on('mouseout',function(e){
                    this.css({
                        backgroundColor: '#ac0'
                    });
                });
                
                opts.text && (this.obj.innerText = opts.text);
                opts.click && (this.on('click' , opts.click));
            } ,
            
            /** 数据列表 * */
            dataGrid : function(opts){
                var $this = this;
                var opts = opts || {};
                var header = null; //表头
                var id = null; //grid的id，唯一
                var tb_id = null;
                var tbody_id = null;
                var count = 0; //为了防止id重复
                var columns = []; //存放field
                var types = [];
                if(!this.obj.id) return;
                else id = this.obj.id;
                
                if(!opts.header) return;
                else header = opts.header;
                
                /**添加基本样式* */
                this.addClass('tableBox');
                
                //初始化表头
                function initHeader(){
                    var time = new Date().getTime();
                    tb_id = 'mui-table_' + time + '_' + count++;
                    var html = " <table id='"+tb_id+"'><thead>" ;
                    
                    //拼接表头
                    html += '<tr>' ;
                    header.forEach(function(item){
                        columns.push(item.field); //添加字段名
                        types.push(item.type);    //添加列类型
                        var width = null;
                        if(item.width) width = item.width + 'px'; //设置宽度
                        if(width) width = "width='"+width+"' ";
                        html += "<th "+width+">" + item.name + '</th>'
                    });
                    tbody_id = 'mui-table-tbody_' + time + '_' + count++;
                    html += "</tr></thread><tbody id='"+tbody_id+"'></tbody>" ;
                    html += '</table>' ;
                    
                    $this.obj.innerHTML = html;
                }
                
                
                //
                initHeader();
                
                return {
                    tbody_id : tbody_id ,
                    allData : null ,
                    ids : [], //保存每一行的id
                    index : 0,//作为行号和id
                    //加载数据
                    load : function(data){
                        this.allData = data;
                        var html = '';
                        //console.log($('#' + tbody_id));
                        var len = data.length; //总行数
                        var columnSize = columns.length;//总列数
                        //alert(len);
                        for(var i = 0;i < len ; i++){
                            this.ids.push('mui-dataGrid-tr_' + ( new Date().getTime() ) + '_' + this.index++) ;
                            //console.log(this.ids[this.index - 1]);
                            //console.log(this.ids[this.index - 1].substring(this.ids[this.index - 1].length - 1 )); //获取行号
                            html += "<tr id='"+this.ids[this.index - 1]+"'>"; /*之前在这里少了一个单引号，最终显示的数据只有全部的一半，现在已经更正*/
                            //遍历列
                            //console.log(types);
                            for(var j = 0; j < columnSize ; j++){
                                var columnName = columns[j];
                                if(data[i][columnName]){
                                    html += '<td>' + data[i][columnName] + '</td>';
                                }else if(types[j] == 'checkColumn'){
                                    html += '<td><input type="checkbox" value=""/></td>';
                                }else {
                                    html += '<td></td>';
                                }
                                
                            }
                            //列遍历完后，这一行才结束
                            html += '</tr>'
                            
                        }
                        
                        //展示数据
                        win.$('#' + this.tbody_id).obj.innerHTML = html;
                        
                        //给每一行添加事件
                        this.ids.forEach(function(rowId){
                            win.$('#' + rowId).on('click',function(){
                                this.toggleClass('selected');
                                if(this.hasClass('selected')){
                                    this.obj.getElementsByTagName('input')[0].checked = true;
                                }else {
                                    this.obj.getElementsByTagName('input')[0].checked = false;
                                }
                            });
                        });
                        
                        
                            
                    } ,
                    
                    //获取所有数据
                    getData : function(){
                        return this.allData;
                    } ,
                    
                    //根据行号获取某一行
                    
                    getRow : function(rowIndex){
                        return this.getData()[rowIndex];
                    } ,
                    
                    //获取所有的行号
                    getSize : function(){
                        var len = 0;
                        this.getSelected && (len = this.getSelected().length) ;
                        return len;
                    } ,
                    
                    //返回选中的行，一条或者多条
                    getSelected : function(){
                        var rows = win.$('.selected').obj; //获取所有选中行
                        var len = 0;
                        len = rows.length;
                        var arr = [];
                        for(var i = 0;i < len;i++){
                            //console.log(rows[i].id.substring(rows[i].id.length - 1));
                            arr.push(this.getRow(rows[i].id.split('_')[2])) ;
                        }
                        
                        arr.length == 1 && ( arr = arr[0] );
                        
                        return arr;
                        
                        //this.ids[this.index - 1].substring(this.ids[this.index - 1].length - 1 )
                    }
                    
                    
                    
                    
                };

            }
            
        }
    }
    
    win.$ = jDom;
    
    win.mui = {
        get : function(sel){
            return jDom(sel);
        }
    }
    
})(window);