package com.example.fallguys.controller;

import com.example.fallguys.dto.SuccessResponseDto;
import com.example.fallguys.dto.user.*;
import com.example.fallguys.exception.BaseResponse;
import com.example.fallguys.exception.BaseResponseCode;
import com.example.fallguys.service.UserService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.Parameters;
import io.swagger.v3.oas.annotations.enums.ParameterIn;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

@RestController
@Tag(name = "1. User")
@RequestMapping(value = "/api/user")
@RequiredArgsConstructor
public class UserController {

    private final UserService userService;

    @Operation(summary = "회원가입", description = "회원가입을 합니다.")
    @PostMapping("/signup")
    public BaseResponse<Long> signup(@Parameter(name = "UserSignupRequestDto", description = "회원 한 명의 정보를 갖는 객체", required = true) @RequestBody UserSignupRequestDto userSignupRequestDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.signUp(userSignupRequestDto));
    }

    @Operation(summary = "닉네임 체크", description = "닉네임 중복 여부 체크")
    @PostMapping("/checknickname")
    public BaseResponse<SuccessResponseDto> nicknameCheck(@Parameter(name = "UserCheckNameRequestDto", description = "회원가입 닉네임", required = true) @RequestBody UserCheckNameRequestDto userCheckNameRequestDto) throws Exception {
        return new BaseResponse(userService.nicknameCheck(userCheckNameRequestDto.getUserNickname()).getStatus(), "요청 성공했습니다.", userService.nicknameCheck(userCheckNameRequestDto.getUserNickname()));
    }

    @Operation(summary = "로그인", description = "이메일로 로그인을 합니다.")
    @PostMapping("/login")
    public BaseResponse<UserLoginResponseDto> login(@Parameter(name = "UserLoginRequestDto", description = "회원 한 명의 정보를 갖는 객체", required = true) @RequestBody UserLoginRequestDto userLoginDto) throws Exception {
        return new BaseResponse(userService.login(userLoginDto).getStatus(), "요청 성공했습니다.", userService.login(userLoginDto));
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "사용자 정보 조회", description = "사용자 정보 단건 조회")
    @GetMapping("/find")
    public BaseResponse<UserResponseDto> findByUser() {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.findByUser());
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "닉네임 변경", description = "사용자 닉네임을 변경합니다.")
    @PutMapping("/update/nickname")
    public BaseResponse<SuccessResponseDto> updateNickname(@Parameter(name = "UserUpdateNameRequestDto", description = "변경할 닉네임", required = true) @RequestBody UserUpdateNameRequestDto userUpdateNameDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateNickname(userUpdateNameDto));
    }
}
