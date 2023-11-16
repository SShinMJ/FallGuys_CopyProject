package com.example.fallguys.controller;

import com.example.fallguys.dto.SuccessResponseDto;
import com.example.fallguys.dto.costumeColor.UserColorResponseDto;
import com.example.fallguys.dto.costumeColor.UserColorUpdateRequestDto;
import com.example.fallguys.dto.costumeColor.UserGetColorRequestDto;
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
    @GetMapping("")
    public BaseResponse<UserResponseDto> findByUser() {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.findByUser());
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "사용자 코스튬 색상 소유 정보 조회", description = "사용자 코스튬 색상 소유 정보 전체 조회")
    @GetMapping("/costume/color")
    public BaseResponse<UserColorResponseDto> findUserColorListByUser() {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.findUserColorListByUser());
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "사용자 코스튬 색상 변경", description = "사용자 코스튬 색상을 변경합니다.")
    @PutMapping("/costume/color")
    public BaseResponse<SuccessResponseDto> updateUserCostumeColor(@Parameter(name = "UserColorUpdateRequestDto", description = "변경할 색상 넘버", required = true) @RequestBody UserColorUpdateRequestDto userColorUpdateRequestDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateUserCostumeColor(userColorUpdateRequestDto));
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "사용자 코스튬 색상 구매 처리", description = "사용자가 코스튬 구매시 데이터를 처리합니다.")
    @PutMapping("/costume/color/get")
    public BaseResponse<SuccessResponseDto> updateGetUserCostumeColor(@Parameter(name = "UserGetColorRequestDto", description = "구입한 색상 및 가격", required = true) @RequestBody UserGetColorRequestDto userGetColorRequestDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateGetUserCostumeColor(userGetColorRequestDto));
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "닉네임 변경", description = "사용자 닉네임을 변경합니다.")
    @PutMapping("/nickname")
    public BaseResponse<SuccessResponseDto> updateNickname(@Parameter(name = "UserUpdateNameRequestDto", description = "변경할 닉네임", required = true) @RequestBody UserUpdateNameRequestDto userUpdateNameDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateNickname(userUpdateNameDto));
    }

    @Parameters({
            @Parameter(
                    name = "X-AUTH-TOKEN",
                    description = "로그인 성공 후 AccessToken",
                    required = true, schema = @Schema(type = "String"), in = ParameterIn.HEADER)
    })
    @Operation(summary = "사용자 쿠도스 보상 처리", description = "사용자 쿠도스(재화)를 얻을 시 데이터를 처리합니다.")
    @PutMapping("/kudos/get")
    public BaseResponse<SuccessResponseDto> updateGetUserKudos(@Parameter(name = "UserGetKudosRequestDto", description = "얻은 쿠도스 값", required = true) @RequestBody UserGetKudosRequestDto userGetKudosRequestDto) throws Exception {
        return new BaseResponse(BaseResponseCode.OK.getHttpStatus(), BaseResponseCode.OK.getMessage(), userService.updateGetUserKudos(userGetKudosRequestDto));
    }
}
