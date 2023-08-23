import { CalendarOutlined } from "@ant-design/icons"
import { CircleProgressBarItem } from "./CircleProgressBarItem"

export const CircleProgressBar = ({ items }) => {
    return (
        <div
            style={{
                display: 'flex',
                overflow: 'auto'
            }}>
            <div
                style={{
                    display: 'flex',
                    flexWrap: 'nowrap',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}
            >
                {items.map((item,index) => (
                    <div style={{ width: '12em' }}
                        key={index}>
                        <CircleProgressBarItem
                            
                            width={item.width}
                            percent={item.percent}
                            title={item.title}
                            subtitle={item.subtitle}
                            status={item.status}
                            icon={<CalendarOutlined />} />
                    </div>
                ))}
            </div>
        </div>

    )
}
