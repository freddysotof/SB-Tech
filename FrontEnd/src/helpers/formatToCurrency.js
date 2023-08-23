
const formatterUS = new Intl.NumberFormat('en-US');

export const formatToCurrency = (value) => {
    if (value)
        return formatterUS.format(value?.toFixed(2));
    else return 0;
}