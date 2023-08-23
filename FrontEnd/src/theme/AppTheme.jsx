import { ConfigProvider } from "antd";
import { customTheme } from "./";
import dayjs from 'dayjs';
import relativeTime from 'dayjs/plugin/relativeTime'
import localizedFormat from 'dayjs/plugin/localizedFormat';
import esES from 'antd/locale/es_ES';
import { dateLocalizer } from "../helpers";
import { SmileOutlined } from "@ant-design/icons";

dayjs.locale(dateLocalizer)
dayjs.extend(relativeTime);
dayjs.extend(localizedFormat);
export const AppTheme = ({ children }) => {
  const customizeRenderEmpty = () => (
    <div
      style={{
        textAlign: 'center',
        height: '100%',
      }}
    >
      <SmileOutlined
        style={{
          fontSize: 20,
          height:'100%'
        }}
      />
      <p>Data Not Found</p>
    </div>
  );
  return (
    <ConfigProvider
      locale={esES}
      theme={customTheme}
      renderEmpty={true ? customizeRenderEmpty : undefined}
    >
      {children}
    </ConfigProvider>)
};
