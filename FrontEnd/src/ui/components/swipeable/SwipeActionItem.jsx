import {
    SwipeAction,
} from 'react-swipeable-list';
export const SwipeActionItem = ({ onClick = () => { }, style = {}, destructive = false, children }) => {
    return (
        <SwipeAction
            destructive={destructive}
            onClick={onClick}>
            <div style={{
                height: '100%',
                display: 'flex',
                alignItems: 'center',
                justifyContent:'center',
                padding: '0.5em',
                fontWeight: '500',
                fontSize: '12px',
                boxSizing: 'border-box',
                color: '#eee',
                userSelect: 'none',
                ...style
            }}>
                <div style={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center'
                }}>
                    {children}
                </div>
            </div>
        </SwipeAction>
    )
}
