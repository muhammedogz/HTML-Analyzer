import { ChangeCircle, ChangeCircleTwoTone } from '@mui/icons-material';
import { Button, Stack, Typography, useMediaQuery, useTheme } from '@mui/material';

import { useState } from 'react';
import { diff as DiffEditorAce } from 'react-ace';

type DiffEditorProps = {
  code: string;
  setCode: (value: string) => void;
  otherCode: string;
  setOtherCode: (value: string) => void;
  initialReadOnly?: boolean;
};

const DiffEditor = ({ code, setCode, otherCode, setOtherCode, initialReadOnly = false }: DiffEditorProps) => {
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

      <DiffEditorAce
        readOnly={readOnly}
        wrapEnabled
        mode="html"
        theme="monokai"
        name="blah2"
        fontSize={16}
        showGutter={true}
        value={[code, otherCode]}
        onChange={(value) => {
          setCode(value[0]);
          setOtherCode(value[1]);
        }}
        // onPaste={(value) => setCode(value)}
        setOptions={{
          enableBasicAutocompletion: true,
          enableLiveAutocompletion: false,
          enableSnippets: false,
          showLineNumbers: true,
          tabSize: 2,
        }}
        style={{
          width: isDesktop ? '800px' : '85vw',
        }}
        height={isDesktop ? '300px' : '40vh'}
      />
    </Stack>
  );
};

export default DiffEditor;
