import { Carousel } from 'antd';
const contentStyle = {
  margin: 0,
  height: '160px',
  color: '#fff',
  lineHeight: '160px',
  textAlign: 'center',
  background: '#364d79',
};
export const EmptyCarousel = ({ style = {}, quantity = 2,position="left" }) => {
  return (
    <Carousel
      infinite={false}
      style={style ?? contentStyle}
      autoplay
      autoplaySpeed={'25'}
      dotPosition={position}
    >
      {[...Array(quantity)].map((val, index) => (
        <div key={index}>
          <h3 style={contentStyle}>{index + 1}</h3>
        </div>
      ))
      }



    </Carousel>

  )
}
