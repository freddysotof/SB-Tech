const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'DOP',
    currencyDisplay: 'code' // default: 'symbol'
});

export const formatToMoney = (value) => {
    if (value)
        return formatter.format(value.toFixed(2));
    else return 0;
}