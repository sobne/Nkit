((function(jUnity){
    jUnity.array=jUnity.array?jUnity.array:{};
    jUnity.extend(jUnity.array,{
        deleteEmpty:function(arr){
            for(var i=0,j=arr.length;i<j;){
                if(arr[i]==""||arr[i]==null){
                    arr.splice(i,1);
                    j=arr.lehgth;
                }
                else{
                    i++;
                }
            }
            return arr;
        }
    });
})(jUnity));