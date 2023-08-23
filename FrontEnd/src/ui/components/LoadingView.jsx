import { Skeleton } from "antd";
export const LoadingView = ({rows=15}) => {
  return (
    <>
    <Skeleton 
    active
    paragraph={{
      rows:rows,
    }}
     />
     </>
  )
}
