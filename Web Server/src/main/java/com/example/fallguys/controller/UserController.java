package com.example.fallguys.controller;

import com.example.fallguys.dto.SuccessResponseDto;
import com.example.fallguys.dto.user.*;
import com.example.fallguys.exception.BaseResponse;
import com.example.fallguys.exception.BaseResponseCode;
import com.example.fallguys.service.UserService;
import io.swagger.annotations.*;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

@RestController
@Api(tags = {"1. User"})
@RequestMapping(value = "/api/user")
@RequiredArgsConstructor
public class UserController {

    private final UserService userService;

    @ApiOperation(value = "회원가입", notes = "회원가입을 합니다.")
    @PostMapping("/signup")
    public BaseResponse<Long> signup(@ApiParam(value = "회원 한 명의 정보를 갖는 객체", required = true) @RequestBody UserSignupRequestDto userSignupRequestDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.signUp(userSignupRequestDto));
    }

    @ApiOperation(value = "닉네임 체크", notes = "닉네임 중복 여부 체크")
    @PostMapping("/checknickname")
    public BaseResponse<SuccessResponseDto> nicknameCheck(@ApiParam(value = "회원가입 닉네임", required = true) @RequestBody UserCheckNameRequestDto userCheckNameRequestDto) throws Exception {
        return new BaseResponse(userService.nicknameCheck(userCheckNameRequestDto.getUserNickname()).getStatus(), "요청 성공했습니다.", userService.nicknameCheck(userCheckNameRequestDto.getUserNickname()));
    }

    @ApiOperation(value = "로그인", notes = "이메일로 로그인을 합니다.")
    @PostMapping("/login")
    public BaseResponse<UserLoginResponseDto> login(@ApiParam(value = "회원 한 명의 정보를 갖는 객체", required = true) @RequestBody UserLoginRequestDto userLoginDto) throws Exception {
        return new BaseResponse(userService.login(userLoginDto).getStatus(), "요청 성공했습니다.", userService.login(userLoginDto));
    }

    @ApiImplicitParams({
            @ApiImplicitParam(
                    name = "X-AUTH-TOKEN",
                    value = "로그인 성공 후 AccessToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @ApiOperation(value = "사용자 정보 조회", notes = "사용자 정보 단건 조회")
    @GetMapping("/find")
    public BaseResponse<UserResponseDto> findByUser() {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.findByUser());
    }

    @ApiImplicitParams({
            @ApiImplicitParam(
                    name = "X-AUTH-TOKEN",
                    value = "로그인 성공 후 AccessToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @ApiOperation(value = "닉네임 변경", notes = "사용자 닉네임을 변경합니다.")
    @PutMapping("/update/nickname")
    public BaseResponse<SuccessResponseDto> updateNickname(@ApiParam(value = "변경할 닉네임", required = true) @RequestBody UserUpdateNameRequestDto userUpdateNameDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateNickname(userUpdateNameDto));
    }
}
