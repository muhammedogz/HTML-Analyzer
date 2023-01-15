import { ChangeCircle, ChangeCircleTwoTone } from '@mui/icons-material';
import { Button, Stack, Typography, useMediaQuery, useTheme } from '@mui/material';
import 'ace-builds/src-noconflict/ext-language_tools';
import 'ace-builds/src-noconflict/mode-html';
import 'ace-builds/src-noconflict/theme-monokai';
import 'prismjs/themes/prism-coy.css';
import { useState } from 'react';
import AceEditor from 'react-ace';

type EditorProps = {
  code: string;
  setCode: (value: string) => void;
  initialReadOnly?: boolean;
};

const Editor = ({ code, setCode, initialReadOnly = false }: EditorProps) => {
  const [readOnly, setReadOnly] = useState(initialReadOnly);
  const isDesktop = useMediaQuery(useTheme().breakpoints.up('md'));

  return (
    <Stack
      sx={{
        position: 'relative',
      }}
    >
      <Stack
        sx={{
          position: 'absolute',
          bottom: '0px',
          right: '0px',
          opacity: 0.8,
          zIndex: 199,
          // make a cute gradient with red and magenta
          background: 'linear-gradient(90deg, #5ca2e8, #ff00ff)',
          borderRadius: '10px 0px 0px 0px',
        }}
      >
        <Button
          onClick={() => {
            setReadOnly(!readOnly);
          }}
        >
          <Stack flexDirection="row" alignItems="center" gap={1} color="white">
            <Typography fontSize="12px">{readOnly ? 'Enable' : 'Disable'} Editor</Typography>
            {/* give a transition effect on change readOnly with icons */}
            {readOnly ? <ChangeCircleTwoTone fontSize="small" /> : <ChangeCircle fontSize="small" />}
          </Stack>
        </Button>
      </Stack>

      <AceEditor
        readOnly={readOnly}
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
          width: isDesktop ? '600px' : '85vw',
        }}
        height={isDesktop ? '300px' : '40vh'}
      />
    </Stack>
  );
};

export default Editor;
