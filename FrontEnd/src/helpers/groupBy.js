export const groupBy = (list,props)=>{
    return list.reduce((a,b)=>{
        (a[b[props]]=a[b[props]] ||[]).push(b);
        return a;
    },{});
}