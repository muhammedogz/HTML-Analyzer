import { Stack, useMediaQuery, useTheme } from '@mui/material';
import 'ace-builds/src-noconflict/ext-language_tools';
import 'ace-builds/src-noconflict/mode-html';
import 'ace-builds/src-noconflict/theme-monokai';
import 'prismjs/themes/prism-coy.css';
import AceEditor from 'react-ace';

type EditorProps = {
  code: string;
  setCode: (value: string) => void;
};

const Editor = ({ code, setCode }: EditorProps) => {
  const isDesktop = useMediaQuery(useTheme().breakpoints.up('md'));

  return (
    <Stack>
      <AceEditor
        wrapEnabled
        placeholder="Enter your HTML"
        mode="html"
        theme="monokai"
        name="blah2"
        fontSize={16}
        showGutter={true}
        value={code}
        onChange={(value) => setCode(value)}
        onPaste={(value) => setCode(value)}
        setOptions={{
          enableBasicAutocompletion: true,
          enableLiveAutocompletion: false,
          enableSnippets: false,
          showLineNumbers: true,
          tabSize: 2,
        }}
        style={{
          width: isDesktop ? '700px' : '85vw',
        }}
        height={isDesktop ? '500px' : '50vh'}
      />
    </Stack>
  );
};

export default Editor;
